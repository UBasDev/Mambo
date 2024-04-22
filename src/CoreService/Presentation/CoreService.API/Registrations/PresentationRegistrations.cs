using CoreService.Application.Models;
using Elastic.Apm.NetCoreAll;
using Mambo.JWT;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog.Sinks.Elasticsearch;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

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