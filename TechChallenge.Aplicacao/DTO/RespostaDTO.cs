namespace TechChallenge.Aplicacao.DTO;

public class RespostaDTO
{
    public Tipos Tipo { get; set; }
    public string Mensagem { get; set; }

    public RespostaDTO(Tipos tipo, string mensagem)
    {
        Tipo = tipo;
        Mensagem = mensagem;
    }

    public enum Tipos
    {
        Erro, Aviso, Sucesso
    }
}
