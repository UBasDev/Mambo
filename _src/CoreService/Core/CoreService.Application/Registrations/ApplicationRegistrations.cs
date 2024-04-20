using CoreService.Application.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreService.Application.Registrations
{
    public static class ApplicationRegistrations
    {
        public static void AddApplicationRegistrations(this IServiceCollection services, string mamboCoreDbConnectionString)
        {
            services.AddDbContext<MamboCoreDbContext>(opt =>
            {
                opt.UseNpgsql(mamboCoreDbConnectionString);
            });
        }
    }
}