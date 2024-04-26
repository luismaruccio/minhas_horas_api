using Microsoft.AspNetCore.Identity;
using MinhasHoras.Domain.DomainServices;
using MinhasHoras.Domain.Entities;
using Moq;

namespace MinhasHoras.Domain.Tests.DomainServices
{
    public class PasswordHasherServiceTests
    {
        private readonly PasswordHasherService _passwordHasherService;
        private readonly Mock<IPasswordHasher<User>> _passwordHasherMock = new Mock<IPasswordHasher<User>>();

        public PasswordHasherServiceTests()
        {
            _passwordHasherService = new PasswordHasherService(_passwordHasherMock.Object);
        }

        [Fact]
        public void HashPassword_ValidPassword_ReturnsHashedPassword()
        {
            var user = new User { Name = "Test", Email = "test@example.com", Password = "password" };
            var hashedPassword = "hashed_password";
            _passwordHasherMock.Setup(ph => ph.HashPassword(user, user.Password)).Returns(hashedPassword);

            var result = _passwordHasherService.HashPassword(user, user.Password);

            Assert.Equal(hashedPassword, result);
        }

        [Fact]
        public void VerifyHashedPassword_ValidPassword_ReturnsSuccess()
        {
            var user = new User { Name = "Test", Email = "test@example.com", Password = "password" };
            var hashedPassword = "hashed_password";
            _passwordHasherMock.Setup(ph => ph.VerifyHashedPassword(user, hashedPassword, user.Password)).Returns(PasswordVerificationResult.Success);

            var result = _passwordHasherService.VerifyHashedPassword(user, hashedPassword, user.Password);

            Assert.Equal(PasswordVerificationResult.Success, result);
        }
    }
}
