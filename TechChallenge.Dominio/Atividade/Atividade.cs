using System.ComponentModel.DataAnnotations;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Atividade;

public class Atividade
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    public Departamentos DepartamentoResponsavel { get; set; }
    public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    public Prioridades Prioridade { get; set; }
    public int PrazoEstimado { get; set; }
    public virtual List<Usuario.Usuario> Solucionadores { get; } = new();

    //public virtual ICollection<RelacionamentoAtividadeUsuario> Solucionadores { get; set; }
    public virtual ICollection<Demanda.Demanda> Demandas { get; set; } = new List<Demanda.Demanda>();

    public Atividade() { }

    public Atividade(
        string nome,
        string descricao,
        bool estahAtiva,
        Departamentos departamentoResponsavel,
        TiposDeDistribuicao tipoDeDistribuicao,
        Prioridades prioridade,
        int prazoEstimado)
    {
        Nome = nome;
        Descricao = descricao;
        EstahAtiva = estahAtiva;
        DepartamentoResponsavel = departamentoResponsavel;
        TipoDeDistribuicao = tipoDeDistribuicao;
        Prioridade = prioridade;
        PrazoEstimado = prazoEstimado;
    }
}
