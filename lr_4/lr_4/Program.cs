using Microsoft.AspNetCore.Mvc;

namespace lr_4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("book.json")
                .AddJsonFile("users.json");
            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
