namespace TechChallenge.Dominio.Exceptions;

public class DemandaNaoEncontradaException : Exception
{
    public DemandaNaoEncontradaException()
    {
    }

    public DemandaNaoEncontradaException(string? message)
        : base(message)
    {
    }

    public DemandaNaoEncontradaException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
