using EmailService.Application.Repositories.SentMailRepository;
using EmailService.Persistence.Repositories.SentMailRepository;
using Mambo.Mongo.Concretes;
using Mambo.Mongo.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Persistence.Registrations
{
    public static class PersistenceRegistrations
    {
        public static void AddPersistenceRegistrations(this IServiceCollection services, MongoDbSettings mongoDbSettings)
        {
            #region MongoDb

            services.AddSingleton(mongoDbSettings);
            services.AddScoped<MongoDbContext>();
            services.AddScoped<ISentMailReadRepository, SentMailReadRepository>();
            services.AddScoped<ISentMailWriteRepository, SentMailWriteRepository>();

            #endregion MongoDb
        }
    }
}