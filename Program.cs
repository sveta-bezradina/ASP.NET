using lr_15.Hubs;
using lr_15.Models;
using lr_15.Services.BackgroundServices;
using lr_15.Services.QuartzJobs;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Додайте контролери, HTTP-клієнт, кеш і SignalR
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();

// Додайте Quartz.NET для планування завдань
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("SendEmailJob");
    q.AddJob<SendEmailJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SendEmailJobTrigger")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(20) // Виконувати кожні 30 секунд
            .RepeatForever()));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// Реєструйте сервіси
builder.Services.AddSingleton<EmailService>();
builder.Services.AddHostedService<DatabaseNotificationService>();
builder.Services.AddHostedService<CheckWebsiteBackgroundService>();
builder.Services.AddHostedService<ApiDataCachingService>();
builder.Services.AddHostedService<NotificationService>();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();


var options = new DbContextOptionsBuilder<MyDbContext>()
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .Options;

using (var context = new MyDbContext(options))
{
    context.Users.Add(new User { Name = "Hello", IsNotified = false });
    context.SaveChanges();
}

// Налаштуйте маршрутизацію та SignalR Hub
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapHub<NotificationHub>("/notifications");
});

app.Run();
