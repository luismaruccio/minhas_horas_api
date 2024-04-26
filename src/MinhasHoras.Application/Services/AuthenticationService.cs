using Microsoft.AspNetCore.Identity;
using MinhasHoras.Application.Exceptions.Users;
using MinhasHoras.Application.Results.Users;
using MinhasHoras.Application.Services.Interfaces;
using MinhasHoras.Domain.DomainServices.Interfaces;
using MinhasHoras.Infra.Repositories.Interfaces;

namespace MinhasHoras.Application.Services
{
    public class AuthenticationService(IUserRepository userRepository, IPasswordHasherService passwordHasherService, IJwtService jwtService) : IAuthenticationService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasherService _passwordHasherService = passwordHasherService;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<UserResult> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email) ?? throw new InvalidCredentialsException();

            if (_passwordHasherService.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Failed)
                throw new InvalidCredentialsException();

            var token = _jwtService.GenerateJwt(user.Id.ToString(), user.Email);

            return new UserResult(user.Id.ToString(), user.Name, user.Email, token);
        }
    }
}
