using MinhasHoras.Domain.Entities;
using MinhasHoras.Infra.Repositories.Interfaces;
using MongoDB.Driver;

namespace MinhasHoras.Infra.Repositories
{
    public class UserRepository(IMongoDatabase mongoContext) : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection = mongoContext.GetCollection<User>("Users");

        public async Task<User> CreateUserAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _usersCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
        }
    }
}
