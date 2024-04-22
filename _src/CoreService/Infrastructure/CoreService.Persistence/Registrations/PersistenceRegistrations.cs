using CoreService.Application.Repositories;
using CoreService.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoreService.Persistence.Registrations
{
    public static class PersistenceRegistrations
    {
        public static void AddPersistenceRegistrations(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}