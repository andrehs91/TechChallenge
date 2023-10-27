using System.Text.Json.Serialization;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Aplicacao.DTO;

public class CriarAtividadeDTO
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Prioridades Prioridade { get; set; }
    public int PrazoEstimado { get; set; }

    public AtividadeDTO ConverterParaAtividadeDTO(string departamentoSolucionador)
    {
        return new AtividadeDTO()
        {
            Nome = Nome,
            Descricao = Descricao,
            EstahAtiva = true,
            DepartamentoSolucionador = departamentoSolucionador,
            TipoDeDistribuicao = TipoDeDistribuicao,
            Prioridade = Prioridade,
            PrazoEstimado = PrazoEstimado
        };
    }
}
