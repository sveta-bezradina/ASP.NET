namespace Lr_5_mvc_.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs", "error.log");
            Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogError(ex, context);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An error occurred while processing the request.");
            }
        }

        private async Task LogError(Exception ex, HttpContext context)
        {
            var errorMessage = $"{DateTime.UtcNow.ToString("o")} - {context.Request.Path} - {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
            await File.AppendAllTextAsync(_logFilePath, errorMessage);
        }
    }
}
