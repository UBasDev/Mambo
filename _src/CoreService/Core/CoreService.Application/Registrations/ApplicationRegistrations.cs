using CoreService.Application.Contexts;
using CoreService.Application.Helpers;
using Mambo.Helper;
using Mambo.MassTransit.Concretes;
using Mambo.MassTransit.Models;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoreService.Application.Registrations
{
    public static class ApplicationRegistrations
    {
        public static void AddApplicationRegistrations(this IServiceCollection services, string mamboCoreDbConnectionString, PublisherMassTransitSettings massTransitSettings)
        {
            #region DbContext

            services.AddDbContext<MamboCoreDbContext>(opt =>
            {
                opt.UseNpgsql(mamboCoreDbConnectionString);
            });

            #endregion DbContext

            #region MediatR

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.NotificationPublisher = new ForeachAwaitPublisher();
            });

            #endregion MediatR

            #region MassTransit

            services.AddSingleton<PublisherEventBusProvider>(serviceProvider =>
            {
                return new PublisherEventBusProvider(massTransitSettings);
            });

            #endregion MassTransit

            #region Helpers

            services.AddScoped<GlobalHelpers>();
            services.AddScoped<LocalHelpers>();

            #endregion Helpers
        }
    }
}