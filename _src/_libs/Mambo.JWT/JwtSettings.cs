using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.JWT
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public string[] ValidIssuers { get; set; }
        public string[] ValidAudiences { get; set; }
        public string NameClaimType { get; set; }
        public string RoleClaimType { get; set; }
        public bool RequireSignedTokens { get; set; }
        public bool LogValidationExceptions { get; set; }
        public string[] ValidTypes { get; set; }
        public string[] ValidAlgorithms { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool MapInboundClaims { get; set; }
        public bool SaveToken { get; set; }
        public string CookieKey { get; set; }
    }
}