using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lr_11.Filter
{
    public class UniqueUserFilter : IActionFilter
    {
        private static HashSet<string> _uniqueUsers = new HashSet<string>();
        public void OnActionExecuted(ActionExecutedContext context) 
        {
            if (context.Controller is Controller controller)
            {
                string content = File.Exists("uniqueUsers.txt")
                    ? File.ReadAllText("uniqueUsers.txt")
                    : "No unique users logged yet.";

                controller.ViewData["UniqueUsersLog"] = content;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var ipUser = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            if (!string.IsNullOrEmpty(ipUser) && _uniqueUsers.Add(ipUser))
            {
                var logMessage = $"Unique users count: {_uniqueUsers.Count}{Environment.NewLine}";
                File.AppendAllText("uniqueUsers.txt", logMessage);
            }
        }
    }
}
