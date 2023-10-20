using System.Text.Json.Serialization;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Aplicacao.DTO;

public class CriarDemandaDTO
{
    public long NumeroDaDemanda { get; set; }
    public int AtividadeId { get; set; }
    public long NumeroDaDemandaReaberta { get; set; }
    public long NumeroDaDemandaDesdobrada { get; set; }
    public DateTime MomentoDeAbertura { get; set; }
    public DateTime MomentoDeFechamento { get; set; }
    public DateTime? Prazo { get; set; }
    public string Detalhes { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))] public Situacoes Situacao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Departamentos DepartamentoSolicitante { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Departamentos DepartamentoResponsavel { get; set; }

    public DemandaDTO ConverterParaDemandaDTO()
    {
        return new DemandaDTO()
        {
            NumeroDaDemanda = NumeroDaDemanda,
            AtividadeId = AtividadeId,
            NumeroDaDemandaReaberta = NumeroDaDemandaReaberta,
            NumeroDaDemandaDesdobrada = NumeroDaDemandaDesdobrada,
            MomentoDeAbertura = MomentoDeAbertura,
            MomentoDeFechamento = MomentoDeFechamento,
            Prazo = Prazo,
            Detalhes = Detalhes,
            Situacao = Situacao,
            DepartamentoSolicitante = DepartamentoSolicitante,
            DepartamentoResponsavel = DepartamentoResponsavel
        };
    }
}
