using Microsoft.Extensions.Logging;

namespace Mambo.CQRS
{
    public abstract class BaseHandler<T>(ILogger<T> logger, ) where T : class
    {
        private readonly ILogger<T> _logger = logger;
    }
}