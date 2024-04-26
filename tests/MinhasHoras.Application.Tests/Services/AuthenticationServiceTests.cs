using Microsoft.AspNetCore.Identity;
using MinhasHoras.Application.Exceptions.Users;
using MinhasHoras.Application.Results.Users;
using MinhasHoras.Application.Services;
using MinhasHoras.Domain.DomainServices.Interfaces;
using MinhasHoras.Domain.Entities;
using MinhasHoras.Infra.Repositories.Interfaces;
using Moq;

namespace MinhasHoras.Application.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IPasswordHasherService> _passwordHasherServiceMock = new();
        private readonly Mock<IJwtService> _jwtServiceMock = new();
        private readonly User _user = new() { Name = "Test", Email = "test@test.com", Password = "T3$te" };
        private readonly AuthenticationService _authenticationService;
        private readonly UserResult _userResult = new(id: "000000000000000000000000", name: "Test", email: "test@test.com", token: "Token");

        public AuthenticationServiceTests()
        {
            _jwtServiceMock.Setup(x => x.GenerateJwt(It.IsAny<string>(), It.IsAny<string>())).Returns("Token");
            _authenticationService = new AuthenticationService(_userRepositoryMock.Object, _passwordHasherServiceMock.Object, _jwtServiceMock.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_ValidUser_ReturnsUserResult()
        {
            _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _passwordHasherServiceMock.Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Success);
            var result = await _authenticationService.AuthenticateAsync(_user.Email, _user.Password);
            Assert.Equivalent(_userResult, result);
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidUser_ThrowsInvalidCredentialsException()
        {
            _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _passwordHasherServiceMock.Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Failed);
            await Assert.ThrowsAsync<InvalidCredentialsException>(() => _authenticationService.AuthenticateAsync(_user.Email, _user.Password));
        }

        [Fact]
        public async Task AuthenticateAsync_NonExistingUser_ThrowsInvalidCredentialsException()
        {
            _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)null);
            await Assert.ThrowsAsync<InvalidCredentialsException>(() => _authenticationService.AuthenticateAsync(_user.Email, _user.Password));
        }

    }
}
