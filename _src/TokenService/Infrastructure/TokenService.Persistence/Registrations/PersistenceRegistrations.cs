using Mambo.Mongo.Concretes;
using Mambo.Mongo.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Application.Repositories.TokenRepository;
using TokenService.Persistence.Repositories.TokenRepository;

namespace TokenService.Persistence.Registrations
{
    public static class PersistenceRegistrations
    {
        public static void AddPersistenceRegistrations(this IServiceCollection services, MongoDbSettings mongoDbSettings)
        {
            #region MongoDb

            services.AddSingleton(mongoDbSettings);
            services.AddScoped<MongoDbContext>();
            services.AddScoped<ITokenReadRepository, TokenReadRepository>();
            services.AddScoped<ITokenWriteRepository, TokenWriteRepository>();

            #endregion MongoDb
        }
    }
}