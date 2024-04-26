using MinhasHoras.Application.Exceptions.Users;
using MinhasHoras.Application.Results.Users;
using MinhasHoras.Application.Services;
using MinhasHoras.Application.Services.Interfaces;
using MinhasHoras.Domain.DomainServices.Interfaces;
using MinhasHoras.Domain.Entities;
using MinhasHoras.Infra.Repositories.Interfaces;
using Moq;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace MinhasHoras.Application.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IPasswordHasherService> _passwordHasherServiceMock = new();
        private readonly Mock<IAuthenticationService> _authenticationServiceMock = new();
        private readonly User _user = new() { Name = "Test", Email = "test@test.com", Password = "T3$te" };
        private readonly UserService _userService;
        private readonly UserResult _userResult = new(id: "id", name: "Test", email: "test@test.com", token: "Token");    
        
        public UserServiceTests()
        {
            _passwordHasherServiceMock.Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>())).Returns("hashed_password");
            _authenticationServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(_userResult);
            _userService = new UserService(_userRepositoryMock.Object, _passwordHasherServiceMock.Object, _authenticationServiceMock.Object);
        }

        [Fact]
        public async Task CreateUser_ValidUser_ReturnsUserResult()
        {
            _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)null);
            var result = await _userService.CreateUser(_user);
            Assert.Equal(_userResult, result);
        }

        [Fact]
        public async Task CreateUser_ExistingUser_ThrowsEmailAlreadyExistsException()
        {
            _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(_user);
            await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => _userService.CreateUser(_user));
        }

        [Fact]
        public async Task GetUserByEmail_ExistingUser_ReturnsUser()
        {
            _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(_user);
            var result = await _userService.GetUserByEmail(_user.Email);
            Assert.Equal(_user, result);
        }

        [Fact]
        public async Task GetUserByEmail_NonExistingUser_ThrowsUserNotFoundException()
        {
            _userRepositoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)null);
            await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.GetUserByEmail(_user.Email));
        }

        [Fact]
        public async Task DeleteUser_ValidId_ThrowsNotImplementedException()
        {
            await Assert.ThrowsAsync<NotImplementedException>(() => _userService.DeleteUser(1));
        }

        [Fact]
        public async Task UpdateUser_ValidUser_ThrowsNotImplementedException()
        {
            await Assert.ThrowsAsync<NotImplementedException>(() => _userService.UpdateUser(_user));
        }

    }
}
