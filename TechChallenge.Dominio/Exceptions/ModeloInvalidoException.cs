namespace TechChallenge.Dominio.Exceptions;

public class ModeloInvalidoException : Exception
{
    public ModeloInvalidoException()
    {
    }

    public ModeloInvalidoException(string? message)
        : base(message)
    {
    }

    public ModeloInvalidoException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
