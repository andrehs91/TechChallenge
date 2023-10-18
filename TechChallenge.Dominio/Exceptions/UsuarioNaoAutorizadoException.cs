namespace TechChallenge.Dominio.Exceptions;

public class UsuarioNaoAutorizadoException : Exception
{
    public UsuarioNaoAutorizadoException()
    {
    }

    public UsuarioNaoAutorizadoException(string? message)
        : base(message)
    {
    }

    public UsuarioNaoAutorizadoException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
