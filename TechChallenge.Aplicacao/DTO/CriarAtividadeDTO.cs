using System.Text.Json.Serialization;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Aplicacao.DTO;

public class CriarAtividadeDTO
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Prioridades Prioridade { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public ContagensDePrazo ContagemDePrazo { get; set; }
    public int PrazoEstimado { get; set; }

    public AtividadeDTO ConverterParaAtividadeDTO(Departamentos departamentoResponsavel)
    {
        return new AtividadeDTO()
        {
            Nome = Nome,
            Descricao = Descricao,
            EstahAtiva = true,
            DepartamentoResponsavel = departamentoResponsavel,
            TipoDeDistribuicao = TipoDeDistribuicao,
            Prioridade = Prioridade,
            ContagemDePrazo = ContagemDePrazo,
            PrazoEstimado = PrazoEstimado
        };
    }
}
