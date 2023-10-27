using System.Text.Json.Serialization;

namespace TechChallenge.Aplicacao.DTO;

public class RespostaDTO
{
    [JsonConverter(typeof(JsonStringEnumConverter))] public TiposDeResposta Tipo { get; set; }
    public string Mensagem { get; set; }

    public RespostaDTO(TiposDeResposta tipo, string mensagem)
    {
        Tipo = tipo;
        Mensagem = mensagem;
    }

    public enum TiposDeResposta
    {
        Erro, Aviso, Sucesso
    }
}
