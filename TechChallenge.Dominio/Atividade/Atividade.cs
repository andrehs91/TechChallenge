using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Atividade;

public class Atividade
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    public string CodigoDoDepartamentoResponsavel { get; set; }
    public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    public Prioridades Prioridade { get; set; }
    public ContagensDePrazo ContagemDePrazo { get; set; }
    public int PrazoEstimado { get; set; }

    public Atividade() { }

    public Atividade(
        string nome,
        string descricao,
        bool estahAtiva,
        string departamentoResponsavel,
        TiposDeDistribuicao tipoDeDistribuicao,
        Prioridades prioridade,
        ContagensDePrazo contagemDePrazo,
        int prazoEstimado)
    {
        Nome = nome;
        Descricao = descricao;
        EstahAtiva = estahAtiva;
        CodigoDoDepartamentoResponsavel = departamentoResponsavel;
        TipoDeDistribuicao = tipoDeDistribuicao;
        Prioridade = prioridade;
        ContagemDePrazo = contagemDePrazo;
        PrazoEstimado = prazoEstimado;
    }
}
