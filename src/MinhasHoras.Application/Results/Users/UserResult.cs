namespace MinhasHoras.Application.Results.Users
{
    public class UserResult(string id, string name, string email, string token)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Token { get; set; } = token;
    }
}
