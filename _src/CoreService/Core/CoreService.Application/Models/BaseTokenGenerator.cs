using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Models
{
    public abstract class BaseTokenGenerator
    {
        protected BaseTokenGenerator()
        {
            Username = string.Empty;
            Email = string.Empty;
            RoleName = null;
            RoleLevel = null;
        }

        public string Username { get; protected set; }
        public string Email { get; protected set; }
        public string? RoleName { get; protected set; }
        public UInt16? RoleLevel { get; protected set; }

        public string GenerateToken(TimeSpan expireTime, string secretKey, string issuer, string audience)
        {
            var tokenDescriptor1 = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim("username", Username),
                        new Claim("email", Email),
                            new Claim("rolename", RoleName ?? string.Empty),
                            new Claim("rolelevel", RoleLevel.ToString() ?? string.Empty),
                        }
                ),
                Expires = DateTime.UtcNow.Add(expireTime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

                    SecurityAlgorithms.HmacSha512
                ),
                Issuer = issuer,

                Audience = audience,

                IssuedAt = DateTime.UtcNow,

                TokenType = JwtRegisteredClaimNames.Typ,

                NotBefore = DateTime.UtcNow
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(

                tokenHandler.CreateToken(tokenDescriptor1) //Burada tokenı elde ederiz.
            );
        }
    }
}