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
    [JsonConverter(typeof(JsonStringEnumConverter))] public ContagensDePrazo ContagemDePrazo { get; set; }
    public int PrazoEstimado { get; set; }

    public AtividadeDTO ConverterParaAtividadeDTO(int id, string codigoDoDepartamentoResponsavel)
    {
        return new AtividadeDTO()
        {
            Id = id,
            Nome = Nome,
            Descricao = Descricao,
            EstahAtiva = EstahAtiva,
            CodigoDoDepartamentoResponsavel = codigoDoDepartamentoResponsavel,
            TipoDeDistribuicao = TipoDeDistribuicao,
            Prioridade = Prioridade,
            ContagemDePrazo = ContagemDePrazo,
            PrazoEstimado = PrazoEstimado
        };
    }
}
