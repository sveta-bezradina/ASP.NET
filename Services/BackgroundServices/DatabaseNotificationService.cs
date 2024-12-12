using Microsoft.EntityFrameworkCore;

public class DatabaseNotificationService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly EmailService _emailService;

    public DatabaseNotificationService(IServiceScopeFactory scopeFactory, EmailService emailService)
    {
        _scopeFactory = scopeFactory;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                // Отримуємо нові записи
                var newRecords = await dbContext.Users
                    .Where(user => !user.IsNotified)
                    .ToListAsync(stoppingToken);

                foreach (var user in newRecords)
                {
                    // Надсилаємо електронний лист
                    await _emailService.SendEmailAsync(
                        "koha2953@gmail.com",
                        "New User Added",
                        $"User {user.Name} has been added to the database."
                    );

                    // Позначаємо запис як сповіщений
                    user.IsNotified = true;
                }

                // Зберігаємо зміни
                await dbContext.SaveChangesAsync(stoppingToken);
            }

            // Затримка перед наступною перевіркою
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
