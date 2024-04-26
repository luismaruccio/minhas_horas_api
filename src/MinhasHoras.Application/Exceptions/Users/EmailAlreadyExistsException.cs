namespace MinhasHoras.Application.Exceptions.Users
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException() : base("Este e-mail já está sendo utilizado por outro usuário")
        {
        }
    }
}
