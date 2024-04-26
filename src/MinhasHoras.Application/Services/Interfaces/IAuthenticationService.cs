using MinhasHoras.Application.Results.Users;

namespace MinhasHoras.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResult> AuthenticateAsync(string email, string password);
    }
}
