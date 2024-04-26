using Microsoft.IdentityModel.Tokens;
using MinhasHoras.Domain.DomainServices.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinhasHoras.Domain.DomainServices
{
    public class JwtService(string secretKey, string issuer, string audience, double expiresInMinutes) : IJwtService
    {
        private readonly string _secretKey = secretKey;
        private readonly string _issuer = issuer;
        private readonly string _audience = audience;
        private readonly double _expiresInMinutes = expiresInMinutes;

        public string GenerateJwt(string userId, string userEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, userEmail)
            }),
                Issuer = _issuer,
                Audience = _audience,
                Expires = DateTime.UtcNow.AddMinutes(_expiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
