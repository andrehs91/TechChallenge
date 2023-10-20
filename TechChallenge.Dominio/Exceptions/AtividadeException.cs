namespace TechChallenge.Dominio.Exceptions;

public class AtividadeException : Exception
{
    public AtividadeException(string? message)
        : base(message)
    {
    }

    public AtividadeException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

}
