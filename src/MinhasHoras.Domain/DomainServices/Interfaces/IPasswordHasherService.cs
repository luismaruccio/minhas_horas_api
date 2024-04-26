using Microsoft.AspNetCore.Identity;
using MinhasHoras.Domain.Entities;

namespace MinhasHoras.Domain.DomainServices.Interfaces
{
    public interface IPasswordHasherService
    {
        public string HashPassword(User user, string password);
        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword);
    }
}
