using lr_9.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<WeatherService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "weather",
    pattern: "Weather",
    defaults: new { controller = "Weather", action = "Index" });

app.Run();
