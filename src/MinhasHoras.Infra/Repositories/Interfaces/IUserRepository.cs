using MinhasHoras.Domain.Entities;

namespace MinhasHoras.Infra.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
