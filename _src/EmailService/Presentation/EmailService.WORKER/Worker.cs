using Mambo.Email.Concretes;
using Mambo.Email.Models;

namespace EmailService.WORKER
{
    public class Worker(ILogger<Worker> _logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var peopleTo1 = new HashSet<string>() { "ugurcan.bas.dev@outlook.com" };
            //var subject1 = "Love from me";
            //var content1 = "This is a test operation. This email will be sent from the code. I love my beautiful Sabrina";
            //await _emailProvider.SendEmailAsync(new NewMail(peopleTo1, subject1, content1), stoppingToken);
            //while (!stoppingToken.IsCancellationRequested)
            _logger.LogInformation("Email Worker has started");
        }
    }
}