using Quartz;
using System;
using System.Threading.Tasks;

namespace lr_15.Services.QuartzJobs
{
    public class SendEmailJob : IJob
    {
        private readonly EmailService _emailService;

        public SendEmailJob(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"{DateTime.Now}: Sending scheduled email...");

            await _emailService.SendEmailAsync("koha2953@gmail.com", "Scheduled Email", "This is a test email from Quartz.NET.");
        }
    }
}
