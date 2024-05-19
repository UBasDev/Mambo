using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Helper
{
    public sealed class Helpers(IHttpContextAccessor _httpContextAccessor)
    {
        public void SetTokenCookiesToResponse(string accessToken, string refreshToken, string accessTokenCookieKey, string refreshTokenCookieKey, UInt16 accessTokenExpireTime, UInt16 refreshTokenExpireTime)
        {
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(
                    key: accessTokenCookieKey,

                    value: accessToken,

                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        IsEssential = true,
                        SameSite = SameSiteMode.None,
                        Domain = "localhost",
                        MaxAge = TimeSpan.FromMinutes(accessTokenExpireTime),
                        Path = "/"
                    }
                );
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(
                key: refreshTokenCookieKey,

                value: refreshToken,

                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Domain = "localhost",
                    MaxAge = TimeSpan.FromMinutes(refreshTokenExpireTime),
                    Path = "/"
                }
            );
        }
    }
}