using CoreService.Application.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoreService.Application.Registrations
{
    public static class ApplicationRegistrations
    {
        public static void AddApplicationRegistrations(this IServiceCollection services, string mamboCoreDbConnectionString)
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
            });

            #endregion MediatR
        }
    }
}