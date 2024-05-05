using CoreService.Application.Constants;
using CoreService.Application.Models;
using Mambo.APM;
using Mambo.JWT;

namespace CoreService.API.Registrations
{
    public static class PresentationRegistrations
    {
        public static void AddPresentationRegistrations(this IServiceCollection services, JwtSettings jwtSettings, CorsOptions corsOptions)
        {
            #region JWT

            services.AddSingleton(jwtSettings);
            services.AddJwtRegistrations(jwtSettings);

            #endregion JWT

            #region Prometheus

            services.AddOpenTelemetryApm();

            #endregion Prometheus

            #region HttpContextAccessor

            services.AddHttpContextAccessor();

            #endregion HttpContextAccessor

            #region Cors

            services.AddCors(opt =>
            {
                opt.AddPolicy(ApplicationConstants.CorsPolicyName, builder =>
                {
                    builder.WithOrigins(corsOptions.AllowedOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            #endregion Cors
        }

        public static void UsePresentationRegistrations(this IApplicationBuilder app)
        {
            app.UseCors(ApplicationConstants.CorsPolicyName);
        }
    }
}