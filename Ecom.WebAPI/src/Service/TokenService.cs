using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecom.Core.src.Entity;
using Ecom.Service.src.Abstraction;
using Microsoft.IdentityModel.Tokens;

namespace Ecom.WebAPI.src.Service
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config { get; set; }
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(User user)
        {

            var issuer = _config.GetSection("Jwt:Issuer").Value;
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var audience = _config.GetSection("Jwt:Audience").Value;
            var tokenHandeler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));
            var singingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.Now.AddDays(2),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = singingKey,
            };
            var token = tokenHandeler.CreateToken(descriptor);
            return tokenHandeler.WriteToken(token);

        }
    }
}