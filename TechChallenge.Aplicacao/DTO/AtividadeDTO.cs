using System.Text.Json.Serialization;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Aplicacao.DTO;

public class AtividadeDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    public string CodigoDoDepartamentoResponsavel { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Prioridades Prioridade { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public ContagensDePrazo ContagemDePrazo { get; set; }
    public int PrazoEstimado { get; set; }

    public static AtividadeDTO EntidadeParaDTO (Atividade atividade)
    {
        return new AtividadeDTO()
        {
            Id = atividade.Id,
            Nome = atividade.Nome,
            Descricao = atividade.Descricao,
            EstahAtiva = atividade.EstahAtiva,
            CodigoDoDepartamentoResponsavel = atividade.CodigoDoDepartamentoResponsavel,
            TipoDeDistribuicao = atividade.TipoDeDistribuicao,
            Prioridade = atividade.Prioridade,
            ContagemDePrazo = atividade.ContagemDePrazo,
            PrazoEstimado = atividade.PrazoEstimado
        };
    }

    public static Atividade DTOParaEntidade(AtividadeDTO atividadeDTO)
    {
        return new Atividade()
        {
            Nome = atividadeDTO.Nome,
            Descricao = atividadeDTO.Descricao,
            EstahAtiva = atividadeDTO.EstahAtiva,
            CodigoDoDepartamentoResponsavel = atividadeDTO.CodigoDoDepartamentoResponsavel,
            TipoDeDistribuicao = atividadeDTO.TipoDeDistribuicao,
            Prioridade = atividadeDTO.Prioridade,
            ContagemDePrazo = atividadeDTO.ContagemDePrazo,
            PrazoEstimado = atividadeDTO.PrazoEstimado
        };
    }
}
