using Microsoft.Extensions.Caching.Memory;

namespace lr_15.Services.BackgroundServices
{
    public class ApiDataCachingService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;

        public ApiDataCachingService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync("https://api.exchangerate-api.com/v4/latest/USD");

                _memoryCache.Set("ExchangeRates", response, TimeSpan.FromSeconds(30));

                Console.WriteLine("Cached exchange rates data.");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
