namespace TechChallenge.Dominio.Exceptions;

public class AtividadeNaoEncontradaException : Exception
{
    public AtividadeNaoEncontradaException()
    {
    }

    public AtividadeNaoEncontradaException(string? message)
        : base(message)
    {
    }

    public AtividadeNaoEncontradaException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
