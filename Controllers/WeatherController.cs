using Microsoft.AspNetCore.Mvc;
using lr_9.Services;
using lr_9.Models;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                ViewData["Error"] = "City cannot be empty";
                return View();
            }

            var weather = await _weatherService.GetWeatherAsync(city);

            if (weather == null)
            {
                ViewData["Error"] = "Could not fetch weather data.";
                return View();
            }

            return View(weather);
        }
    }
}
