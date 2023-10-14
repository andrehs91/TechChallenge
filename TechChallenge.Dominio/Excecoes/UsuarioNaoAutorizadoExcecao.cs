namespace TechChallenge.Dominio.Excecoes;

public class UsuarioNaoAutorizadoExcecao : Exception
{
    public UsuarioNaoAutorizadoExcecao()
    {
    }

    public UsuarioNaoAutorizadoExcecao(string? message)
        : base(message)
    {
    }

    public UsuarioNaoAutorizadoExcecao(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
