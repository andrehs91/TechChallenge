using System.Text.Json.Serialization;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Aplicacao.DTO;

public class EditarAtividadeDTO
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Prioridades Prioridade { get; set; }
    public int PrazoEstimado { get; set; }

    public AtividadeDTO ConverterParaAtividadeDTO(int id, string departamentoSolucionador)
    {
        return new AtividadeDTO()
        {
            Id = id,
            Nome = Nome,
            Descricao = Descricao,
            EstahAtiva = EstahAtiva,
            DepartamentoSolucionador = departamentoSolucionador,
            TipoDeDistribuicao = TipoDeDistribuicao,
            Prioridade = Prioridade,
            PrazoEstimado = PrazoEstimado
        };
    }
}
