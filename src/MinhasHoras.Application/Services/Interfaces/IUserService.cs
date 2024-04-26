using MinhasHoras.Application.Results.Users;
using MinhasHoras.Domain.Entities;

namespace MinhasHoras.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResult> CreateUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
