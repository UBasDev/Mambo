using CoreService.Application.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Models
{
    internal abstract class BaseCqrsHandler<T>(ILogger<T> logger, IUnitOfWork unitOfWork) where T : class
    {
        protected readonly ILogger<T> _logger = logger;
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;

        //Status code 200~
        protected void LogInformation(string message, object request, HttpStatusCode statusCode) => _logger.LogInformation("Message: {@Message}. Request: {@Request}. Status: {@StatusCode}", message, request, statusCode);

        //Status code 400~
        protected void LogWarning(string message, object request, HttpStatusCode statusCode) => _logger.LogWarning("Message: {@Message}. Request: {@Request}. Status: {@StatusCode}", message, request, statusCode);

        //Status code 400~
        protected void LogWarning(string message, string errorMessage, object request, HttpStatusCode statusCode) => _logger.LogWarning("Message: {@Message}. ErrorMessage: {@ErrorMessage}. Request: {@Request}. Status: {@StatusCode}", message, errorMessage, request, statusCode);

        //Status code 500~
        protected void LogError(string message, Exception exception, object request, HttpStatusCode statusCode) => _logger.LogError("Message: {@Message}. Error: {@Exception}. Request: {@Request}. Status: {@StatusCode}", message, exception, request, statusCode);
    }
}