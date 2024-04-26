namespace MinhasHoras.Application.Exceptions.Users
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Usuário não foi encontrado")
        {
        }
    }
}
