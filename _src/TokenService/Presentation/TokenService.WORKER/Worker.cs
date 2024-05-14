using TokenService.Application.Repositories.Token;
using TokenService.Domain;

namespace TokenService.WORKER
{
    public class Worker(ILogger<Worker> _logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            /*while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }*/
            _logger.LogInformation("Token Worker running at: {time}", DateTimeOffset.Now);
        }
    }
}