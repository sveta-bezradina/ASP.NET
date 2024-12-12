using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace lr_15.Services.BackgroundServices
{
    public class CheckWebsiteBackgroundService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string LogFilePath = "website_status.log";

        public CheckWebsiteBackgroundService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var client = _httpClientFactory.CreateClient();
                    var response = await client.GetAsync("https://example.com", stoppingToken);
                    var status = response.IsSuccessStatusCode ? "Available" : "Unavailable";

                    File.AppendAllText(LogFilePath, $"{DateTime.Now}: Website status - {status}\n");
                }
                catch (Exception ex)
                {
                    File.AppendAllText(LogFilePath, $"{DateTime.Now}: Error - {ex.Message}\n");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
