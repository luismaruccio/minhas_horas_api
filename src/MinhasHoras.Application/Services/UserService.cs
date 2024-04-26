using MinhasHoras.Application.Exceptions.Users;
using MinhasHoras.Application.Results.Users;
using MinhasHoras.Application.Services.Interfaces;
using MinhasHoras.Domain.DomainServices.Interfaces;
using MinhasHoras.Domain.Entities;
using MinhasHoras.Infra.Repositories.Interfaces;

namespace MinhasHoras.Application.Services
{
    public class UserService(IUserRepository userRepository, IPasswordHasherService passwordHasherService, IAuthenticationService authenticationService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasherService _passwordHasherService = passwordHasherService;
        private readonly IAuthenticationService _authenticationService = authenticationService;

        public async Task<UserResult> CreateUser(User user)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
                throw new EmailAlreadyExistsException();
            var userPassword = user.Password;
            user.Password = _passwordHasherService.HashPassword(user, user.Password);
            await _userRepository.CreateUserAsync(user);
            return await _authenticationService.AuthenticateAsync(user.Email, userPassword);
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user ?? throw new UserNotFoundException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
