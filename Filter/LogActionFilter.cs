using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lr_11.Filter
{
    public class LogActionFilter: IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) 
        {
            if (context.Controller is Controller controller)
            {
                string content = File.Exists("actionLog.txt")
                    ? File.ReadAllText("actionLog.txt")
                    : "No action logged yet";

                controller.ViewData["ActionLog"] = content;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var timestamp = DateTime.Now;
            var logMessage = $"Action {actionName}, Timestamp: {timestamp}{Environment.NewLine}";
            File.AppendAllText("actionLog.txt", logMessage);
        }
    }
}
