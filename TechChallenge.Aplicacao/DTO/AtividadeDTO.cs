using System.Text.Json.Serialization;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.DTO;

public class AtividadeDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    public Departamentos DepartamentoResponsavel { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Prioridades Prioridade { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public ContagensDePrazo ContagemDePrazo { get; set; }
    public int PrazoEstimado { get; set; }
    public IList<Usuario> Solucionadores { get; set; } = new List<Usuario>();

    public static AtividadeDTO EntidadeParaDTO (Atividade atividade)
    {
        return new AtividadeDTO()
        {
            Id = atividade.Id,
            Nome = atividade.Nome,
            Descricao = atividade.Descricao,
            EstahAtiva = atividade.EstahAtiva,
            DepartamentoResponsavel = atividade.DepartamentoResponsavel,
            TipoDeDistribuicao = atividade.TipoDeDistribuicao,
            Prioridade = atividade.Prioridade,
            ContagemDePrazo = atividade.ContagemDePrazo,
            PrazoEstimado = atividade.PrazoEstimado,
            Solucionadores = atividade.Solucionadores
        };
    }

    public static Atividade DTOParaEntidade(AtividadeDTO atividadeDTO)
    {
        return new Atividade()
        {
            Nome = atividadeDTO.Nome,
            Descricao = atividadeDTO.Descricao,
            EstahAtiva = atividadeDTO.EstahAtiva,
            DepartamentoResponsavel = atividadeDTO.DepartamentoResponsavel,
            TipoDeDistribuicao = atividadeDTO.TipoDeDistribuicao,
            Prioridade = atividadeDTO.Prioridade,
            ContagemDePrazo = atividadeDTO.ContagemDePrazo,
            PrazoEstimado = atividadeDTO.PrazoEstimado,
        };
    }
}
