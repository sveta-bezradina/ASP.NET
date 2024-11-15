using lr_9.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using lr_9.Models;

namespace lr_9.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "4cce72e3c20870301c9e6629cb46a449"; // Ваш API ключ

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherModel> GetWeatherAsync(string city)
        {
            // Запит до API OpenWeather
            var response = await _httpClient.GetStringAsync(
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=uk");

            // Десеріалізація JSON відповіді в модель
            var weather = JsonConvert.DeserializeObject<WeatherModel>(response);

            return weather;
        }
    }
}
