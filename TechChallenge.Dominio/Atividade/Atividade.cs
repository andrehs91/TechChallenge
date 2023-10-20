using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Atividade;

public class Atividade
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    public Departamentos DepartamentoResponsavel { get; set; }
    public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    public Prioridades Prioridade { get; set; }
    public ContagensDePrazo ContagemDePrazo { get; set; }
    public int PrazoEstimado { get; set; }
    //public IList<Demanda.Demanda> Demandas { get; set; } = new List<Demanda.Demanda>();
    public IList<Usuario.Usuario> Solucionadores { get; set; } = new List<Usuario.Usuario>();

    public Atividade() { }

    public Atividade(
        string nome,
        string descricao,
        bool estahAtiva,
        Departamentos departamentoResponsavel,
        TiposDeDistribuicao tipoDeDistribuicao,
        Prioridades prioridade,
        ContagensDePrazo contagemDePrazo,
        int prazoEstimado)
    {
        Nome = nome;
        Descricao = descricao;
        EstahAtiva = estahAtiva;
        DepartamentoResponsavel = departamentoResponsavel;
        TipoDeDistribuicao = tipoDeDistribuicao;
        Prioridade = prioridade;
        ContagemDePrazo = contagemDePrazo;
        PrazoEstimado = prazoEstimado;
    }

    public DateTime getPrazoAtividade()
    {
        switch (ContagemDePrazo)
        {
            case ContagensDePrazo.DiasCorridos:
                return DateTime.UtcNow.AddDays(PrazoEstimado);
            case ContagensDePrazo.DiasUteis:
                return DateTime.UtcNow.AddDays(PrazoEstimado);
            default:
                return DateTime.UtcNow;
        }
    }
}
