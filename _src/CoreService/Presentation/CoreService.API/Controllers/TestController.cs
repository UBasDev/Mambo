using Mambo.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace CoreService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(ILogger<TestController> logger) : ControllerBase
    {
        private readonly ILogger<TestController> _logger = logger;

        [HttpGet("authorized1")]
        [CustomAuthorizeAttribute("manager1", "admin1")]
        public IActionResult Authorized1()
        {
            return Ok();
        }

        [HttpGet("info1")]
        public IActionResult Info1()
        {
            _logger.LogInformation("Custom info88");
            return Ok();
        }

        [HttpGet("debug1")]
        public IActionResult Debug1()
        {
            _logger.LogDebug("Custom debug88");
            return Ok();
        }

        [HttpGet("error1")]
        public IActionResult Error1()
        {
            _logger.LogError("Custom exception88");
            throw new Exception("Custom exception88");
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