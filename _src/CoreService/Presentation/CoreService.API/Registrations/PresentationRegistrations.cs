using Mambo.JWT;

namespace CoreService.API.Registrations
{
    public static class PresentationRegistrations
    {
        public static void AddPresentationRegistrations(this IServiceCollection services, JwtSettings jwtSettings)
        {
            #region JWT

            services.AddSingleton(jwtSettings);
            services.AddJwtRegistrations(jwtSettings);

            #endregion JWT
        }
    }
}