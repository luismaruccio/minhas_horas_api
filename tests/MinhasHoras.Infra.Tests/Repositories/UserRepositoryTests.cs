using MinhasHoras.Domain.Entities;
using MinhasHoras.Infra.Repositories;

namespace MinhasHoras.Infra.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;
        private readonly MongoDbContainerTest _mongoDbContainerTest = new();

        public UserRepositoryTests()
        {
            Task.Run(() => _mongoDbContainerTest.InitializeAsync()).GetAwaiter().GetResult();
            _userRepository = new UserRepository(_mongoDbContainerTest.GetDatabase());
        }

        [Fact]
        public async Task CreateUserAsync_ValidUser_ReturnsUser()
        {
            var user = new User() { Name = "Teste", Email = "test@example.com", Password = "password" };

            var result = await _userRepository.CreateUserAsync(user);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByEmailAsync_ValidEmail_ReturnsUser()
        {
            var email = "test@example.com";
            var user = new User { Name = "Teste", Email = email, Password = "password" };
            await _userRepository.CreateUserAsync(user);

            var result = await _userRepository.GetUserByEmailAsync(email);

            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
        }
    }
}

