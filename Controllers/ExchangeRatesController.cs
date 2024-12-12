using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace lr_15.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ExchangeRatesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult GetExchangeRates()
        {
            if (_memoryCache.TryGetValue("ExchangeRates", out string exchangeRates))
            {
                return Ok(exchangeRates);
            }

            return NotFound("Exchange rates not found in cache.");
        }
    }
}
