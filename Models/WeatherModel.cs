namespace lr_9.Models
{
    public class WeatherModel
    {
        public string Name { get; set; }
        public MainWeatherInfo Main { get; set; }
        public Weather[] Weather { get; set; }
    }

    public class MainWeatherInfo
    {
        public decimal Temp { get; set; }
        public decimal Humidity { get; set; }
    }

    public class Weather 
    {
        public string Description { get; set; }
    }
}
