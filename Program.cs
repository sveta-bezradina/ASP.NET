using Lr_5_mvc_.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseMiddleware<ErrorLoggingMiddleware>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "test",
    pattern: "{controller=Test}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "test",
    pattern: "{controller=Test}/{action=Index}/{id?}");


app.Run();
