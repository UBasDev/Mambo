using Mambo.MassTransit.Concretes;
using Mambo.MassTransit.Models;
using Mambo.Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Application.Repositories.Token;
using TokenService.Persistence.Repositories.Token;
using TokenService.WORKER.Consumers;

namespace TokenService.WORKER.Registrations
{
    public static class PresentationRegistrations
    {
        public static void AddPresentationRegistrations(this IServiceCollection services, MongoDbSettings mongoDbSettings, ConsumerMassTransitSettings massTransitSettings)
        {
            #region MassTransit

            services.AddEventBusForConsumers<SendUserTokenConsumer>(massTransitSettings);

            #endregion MassTransit

            #region MongoDb

            services.AddSingleton(mongoDbSettings);
            services.AddScoped<ITokenReadRepository, TokenReadRepository>();
            services.AddScoped<ITokenWriteRepository, TokenWriteRepository>();

            #endregion MongoDb
        }
    }
}