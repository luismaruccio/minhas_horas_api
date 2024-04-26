using MongoDB.Bson;

namespace MinhasHoras.Domain.Entities
{
    public class User
    {
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
