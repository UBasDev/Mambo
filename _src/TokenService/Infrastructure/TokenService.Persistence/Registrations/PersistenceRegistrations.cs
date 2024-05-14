using Mambo.Mongo.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Application.Repositories.Token;
using TokenService.Persistence.Repositories.Token;

namespace TokenService.Persistence.Registrations
{
    public static class PersistenceRegistrations
    {
        public static void AddPersistenceRegistrations(this IServiceCollection services, MongoDbSettings mongoDbSettings)
        {
            #region MongoDb

            services.AddSingleton(mongoDbSettings);
            services.AddScoped<ITokenReadRepository, TokenReadRepository>();
            services.AddScoped<ITokenWriteRepository, TokenWriteRepository>();

            #endregion MongoDb
        }
    }
}