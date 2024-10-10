using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace lr_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<CalcService>();
            builder.Services.AddControllers();

            builder.Services.AddTransient<ITimeServise, Time>();
            builder.Services.AddTransient<TimeMessage>();

            var app = builder.Build();
            app.UseRouting();
            app.MapControllers();

            app.MapGet("/day", async context => {
                var timeMessage = context.RequestServices.GetService<TimeMessage>();
                await context.Response.WriteAsync(timeMessage.getTimeOfDay()); 
            });

            app.Run();

        }
    }
    class TimeMessage {
        ITimeServise timeS;

        public TimeMessage(ITimeServise _timeS)
        {
            this.timeS = _timeS;
        }
        public string getTimeOfDay() {

            string timeStr = timeS.getTimeOfDay();

            if(DateTime.TryParse(timeStr, out DateTime time))
            {
                int hour = time.Hour;
                if (hour >= 12 && hour < 18) return "It's daytime";
                else if (hour >= 18 && hour < 24) return "It's evening";
                else if (hour >= 24 && hour < 6) return "It's night";
                else if (hour >= 6 && hour < 12) return "It's morning";
            }
            return "Невідомий час";

        }
    }
    interface ITimeServise {
       string getTimeOfDay();
    }
    public class Time: ITimeServise
    {
        public string getTimeOfDay() => DateTime.Now.ToShortTimeString();
    }
    public class CalcService {
        public int Add(int x, int y) {
            return x + y;
        }
        public int Subtract(int x, int y) {
            return x - y;
        }
        public int Multiply(int x, int y) {
            return x * y;
        }
        public int Divide(int x, int y) {
            if (y == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return x / y;
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CalcController : ControllerBase
    {
        private readonly CalcService _calcService;

        public CalcController(CalcService calcService)
        {
            _calcService = calcService;
        }

        [HttpGet("add")]
        public IActionResult Add(int a, int b)
        {
            var result = _calcService.Add(a, b);
            return Content(result.ToString());
            // Http-запит api/calc/add?a=10&b=5
        }

        [HttpGet("sub")]
        public IActionResult Subtract(int a, int b)
        {
            var result = _calcService.Subtract(a, b);
            return Content(result.ToString());
            // Http-запит api/calc/sub?a=10&b=5
        }

        [HttpGet("mult")]
        public IActionResult Multiply(int a, int b)
        {
            var result = _calcService.Multiply(a, b);
            return Content(result.ToString());
            // Http-запит api/calc/mult?a=10&b=5
        }

        [HttpGet("div")]
        public IActionResult Divide(int a, int b)
        {
            try
            {
                var result = _calcService.Divide(a, b);
                return Content(result.ToString());
                // Http-запит api/calc/div?a=10&b=5
            }
            catch (DivideByZeroException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

