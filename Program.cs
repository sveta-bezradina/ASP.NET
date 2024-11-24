using Serilog;
using Serilog.Exceptions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console()
    .WriteTo.Email(
        from: "koha2953@gmail.com",
        to: "koha2953@gmail.com",
        host: "smtp.gmail.com",
        port: 587,
        connectionSecurity: MailKit.Security.SecureSocketOptions.StartTls,
        credentials: new NetworkCredential(
          "koha2953@gmail.com", "qeng ekqc qdsn pvkv"))
    .CreateLogger();

Log.Information("User {Name} logged in at {Time}", DateTime.Now);


builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
