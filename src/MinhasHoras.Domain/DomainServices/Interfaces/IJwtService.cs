namespace MinhasHoras.Domain.DomainServices.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwt(string userId, string userEmail);
    }
}
