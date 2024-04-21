using Mambo.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("authorized1")]
        [CustomAuthorizeAttribute("manager1", "admin1")]
        public IActionResult Authorized1()
        {
            return Ok();
        }

        [HttpGet("get-token1")]
        public IActionResult GetToken1()
        {
            var secretKey = Encoding.ASCII.GetBytes("your_super_secret_key_here_kjalksdjlaksjdalks_asdkmaskldmsalkmdlskamdklsamdlkjansjkdnaksjndkajsndskajndaskjndjksa");
            var claims = new[] {
                new Claim(ClaimTypes.Name, "ali1"),
                new Claim("prop1", "propValue1"),
                new Claim("roleClaim1", "admin1")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512),
                Issuer = "https://localhost:7186",
                Audience = "https://localhost:7186",
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                TokenType = JwtRegisteredClaimNames.Typ
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(tokenString);
        }

        [HttpGet("get-token-with-cookie1")]
        public IActionResult GetTokenWithCookie1()
        {
            var secretKey = Encoding.ASCII.GetBytes("your_super_secret_key_here_kjalksdjlaksjdalks_asdkmaskldmsalkmdlskamdklsamdlkjansjkdnaksjndkajsndskajndaskjndjksa");
            var claims = new[] {
                new Claim(ClaimTypes.Name, "ali1"),
                new Claim("prop1", "propValue1"),
                new Claim("roleClaim1", "admin1")
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512),
                Issuer = "https://localhost:7186",
                Audience = "https://localhost:7186",
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                TokenType = JwtRegisteredClaimNames.Typ
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            Response.Cookies.Append("MAMBO-ACCESS-TOKEN", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromMinutes(1),
                IsEssential = true,
                Domain = "localhost",
                Path = "/",
            });
            return Ok(tokenString);
        }
    }
}