using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Models
{
    public class CookieSettings
    {
        public CookieSettings()
        {
            AccessTokenCookieKey = string.Empty;
            RefreshTokenCookieKey = string.Empty;
        }

        public string AccessTokenCookieKey { get; set; }
        public string RefreshTokenCookieKey { get; set; }
    }
}