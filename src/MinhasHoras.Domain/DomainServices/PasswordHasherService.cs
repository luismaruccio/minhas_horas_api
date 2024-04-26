using Microsoft.AspNetCore.Identity;
using MinhasHoras.Domain.DomainServices.Interfaces;
using MinhasHoras.Domain.Entities;

namespace MinhasHoras.Domain.DomainServices
{
    public class PasswordHasherService(IPasswordHasher<User> passwordHasher) : IPasswordHasherService
    {
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }
    }
}
