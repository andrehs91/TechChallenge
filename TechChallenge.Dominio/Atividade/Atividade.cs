using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Atividade;

public class Atividade
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    public Departamentos DepartamentoSolucionador { get; set; }
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
        Departamentos departamentoSolucionador,
        TiposDeDistribuicao tipoDeDistribuicao,
        Prioridades prioridade,
        int prazoEstimado)
    {
        Nome = nome;
        Descricao = descricao;
        EstahAtiva = estahAtiva;
        DepartamentoSolucionador = departamentoSolucionador;
        TipoDeDistribuicao = tipoDeDistribuicao;
        Prioridade = prioridade;
        PrazoEstimado = prazoEstimado;
    }
}
