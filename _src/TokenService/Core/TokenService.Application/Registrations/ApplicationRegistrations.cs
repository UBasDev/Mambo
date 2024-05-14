using Mambo.MassTransit.Concretes;
using Mambo.MassTransit.Models;
using Mambo.Mongo.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Application.Consumers;

namespace TokenService.Application.Registrations
{
    public static class ApplicationRegistrations
    {
        public static void AddApplicationRegistrations(this IServiceCollection services, ConsumerMassTransitSettings massTransitSettings)
        {
            #region MassTransit

            services.AddEventBusForConsumers<SendUserTokenConsumer>(massTransitSettings);

            #endregion MassTransit
        }
    }
}