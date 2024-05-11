using Mambo.Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Application.Repositories.Token;
using TokenService.Persistence.Repositories.Token;

namespace TokenService.WORKER.Registrations
{
    public static class PresentationRegistrations
    {
        public static void AddPresentationRegistrations(this IServiceCollection services, MongoDbSettings mongoDbSettings)
        {
            services.AddSingleton(mongoDbSettings);
            services.AddScoped<ITokenReadRepository, TokenReadRepository>();
            services.AddScoped<ITokenWriteRepository, TokenWriteRepository>();
        }
    }
}