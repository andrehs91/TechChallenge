using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Entities;

public class Atividade
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    public string DepartamentoSolucionador { get; set; }
    public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    public Prioridades Prioridade { get; set; }
    public int PrazoEstimado { get; set; }
    public virtual List<Usuario> Solucionadores { get; } = new();
    public virtual List<Demanda> Demandas { get; set; } = new();

    public Atividade() { }

    public Atividade(
        string nome,
        string descricao,
        bool estahAtiva,
        string departamentoSolucionador,
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
