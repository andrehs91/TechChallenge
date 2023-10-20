namespace TechChallenge.Dominio.Exceptions;

public class UsuarioNaoEncontradoException : Exception
{
    public UsuarioNaoEncontradoException()
    {
    }

    public UsuarioNaoEncontradoException(string? message)
        : base(message)
    {
    }

    public UsuarioNaoEncontradoException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
