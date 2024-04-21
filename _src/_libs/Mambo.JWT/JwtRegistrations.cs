using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.JWT
{
    public static class JwtRegistrations
    {
        public static void AddJwtRegistrations(this IServiceCollection services, JwtSettings jwtSettings)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidateLifetime = jwtSettings.ValidateLifetime,
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                //ValidIssuer = "https://localhost:7186",
                ValidIssuers = jwtSettings.ValidIssuers,
                //ValidAudience = "https://localhost:7186",
                ValidAudiences = jwtSettings.ValidAudiences,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                ClockSkew = TimeSpan.Zero,
                NameClaimType = jwtSettings.NameClaimType,
                RoleClaimType = jwtSettings.RoleClaimType,
                RequireSignedTokens = jwtSettings.RequireSignedTokens,
                LogValidationExceptions = jwtSettings.LogValidationExceptions,
                ValidTypes = jwtSettings.ValidTypes,
                ValidAlgorithms = jwtSettings.ValidAlgorithms
            };

            services.AddAuthentication(authOpt =>
            {
                authOpt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddCookie(cookieOpt =>
                {
                    //cookieOpt.Cookie.Name = "cookieKey1";
                    //cookieOpt.Cookie.HttpOnly = true;
                    //cookieOpt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    //cookieOpt.Cookie.SameSite = SameSiteMode.Strict;
                    //cookieOpt.Cookie.Path = "/";
                    //cookieOpt.Cookie.Domain = "localhost";
                    //cookieOpt.Cookie.MaxAge = TimeSpan.FromSeconds(15);

                    //cookieOpt.Cookie.IsEssential = true;
                    //options.LoginPath = "/Account/Login"; // Customize as per your application
                    //options.LogoutPath = "/Account/Logout"; // Customize as per your application
                    //options.AccessDeniedPath = "/Account/AccessDenied"; // Customize as per your application
                    //options.ReturnUrlParameter = "returnUrl"; // Example property usage
                    //cookieOpt.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Example property usage
                    //cookieOpt.SlidingExpiration = true; // Example property usage
                    cookieOpt.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = context =>
                        {
                            // Custom logic for validating the cookie principal
                            //string jwtToken = context.Request.Cookies["cookie1"];
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = context =>
                        {
                            // Custom logic when access is denied
                            return Task.CompletedTask;
                        },
                        OnRedirectToLogin = context =>
                        {
                            // Custom logic when redirecting to the login path
                            return Task.CompletedTask;
                        }
                    };
                })
            .AddJwtBearer(options =>
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
                options.RequireHttpsMetadata = jwtSettings.RequireHttpsMetadata;
                options.MapInboundClaims = jwtSettings.MapInboundClaims;
                options.SaveToken = jwtSettings.SaveToken;
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = (context) =>
                    {
                        if (!string.IsNullOrEmpty(context.Request.Cookies[jwtSettings.CookieKey])) context.Token = context.Request.Cookies[jwtSettings.CookieKey];
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}