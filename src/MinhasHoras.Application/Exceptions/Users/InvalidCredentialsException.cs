namespace MinhasHoras.Application.Exceptions.Users
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("E-mail ou senha inválidos. Verifique suas credenciais e tente novamente.")
        {
        }
    }
}
