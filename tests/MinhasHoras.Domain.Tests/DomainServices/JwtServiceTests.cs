using MinhasHoras.Domain.DomainServices;
using System.IdentityModel.Tokens.Jwt;

namespace MinhasHoras.Domain.Tests.DomainServices
{
    public class JwtServiceTests
    {
        private readonly JwtService _jwtService;
        private readonly string _secretKey = "bb813c390d13874229c5b44ffb8c3fcd";
        private readonly string _issuer = "my_issuer";
        private readonly string _audience = "my_audience";
        private readonly double _expiresInMinutes = 60;

        public JwtServiceTests()
        {
            _jwtService = new JwtService(_secretKey, _issuer, _audience, _expiresInMinutes);
        }

        [Fact]
        public void GenerateJwt_ValidUser_ReturnsJwt()
        {
            var userId = "1";
            var userEmail = "test@example.com";

            var result = _jwtService.GenerateJwt(userId, userEmail);

            Assert.NotNull(result);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(result);

            Assert.Equal(userId, token.Claims.First(c => c.Type == "nameid").Value);
            Assert.Equal(userEmail, token.Claims.First(c => c.Type == "email").Value);
        }
    }
}
