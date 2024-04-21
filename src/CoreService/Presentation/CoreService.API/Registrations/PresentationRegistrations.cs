using CoreService.Application.Models;
using Mambo.JWT;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreService.API.Registrations
{
    public static class PresentationRegistrations
    {
        public static void AddPresentationRegistrations(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddSingleton(jwtSettings);
            services.AddJwtRegistrations(jwtSettings);
        }
    }
}