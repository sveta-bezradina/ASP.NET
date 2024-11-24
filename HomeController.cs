using lr_13.Models;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lr_13.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("÷е information лог");
            _logger.LogDebug("÷е debug лог");
            _logger.LogWarning("÷е warning лог");
            _logger.LogError("÷е error лог");
            _logger.LogCritical("÷е critical лог");

            _logger.LogInformation("User logged in at {Time}", DateTime.Now);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
