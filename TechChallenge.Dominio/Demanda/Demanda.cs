using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Demanda;

public class Demanda
{
    // MOVER CÓDIGO PARA COMANDOS
    //private static readonly Dictionary<Situacoes, CategoriasDeSituacoes> SituacoesESuasCategorias = new()
    //{
    //    { Situacoes.AguardandoDistribuicao, CategoriasDeSituacoes.Ativa },
    //    { Situacoes.EmAtendimento, CategoriasDeSituacoes.Ativa },
    //    { Situacoes.EncaminhadaPeloGestor, CategoriasDeSituacoes.Transitoria },
    //    { Situacoes.EncaminhadaPeloSolucionador, CategoriasDeSituacoes.Transitoria },
    //    { Situacoes.Capturada, CategoriasDeSituacoes.Transitoria },
    //    { Situacoes.Rejeitada, CategoriasDeSituacoes.Transitoria },
    //    { Situacoes.Respondida, CategoriasDeSituacoes.Inativa },
    //    { Situacoes.CanceladaPeloGestor, CategoriasDeSituacoes.Inativa },
    //    { Situacoes.CanceladaPeloSolucionador, CategoriasDeSituacoes.Inativa },
    //    { Situacoes.CanceladaPeloSolicitante, CategoriasDeSituacoes.Inativa },
    //};

    public Atividade.Atividade Atividade { get; set; }
    public long NumeroDaDemanda { get; set; }
    public IList<EventoRegistrado.EventoRegistrado>? Historico { get; set; }
    public long NumeroDaDemandaReaberta { get; set; }
    public long NumeroDaDemandaDesdobrada { get; set; }
    public DateTime MomentoDeAbertura { get; set; }
    public DateTime MomentoDeFechamento { get; set; }
    public DateTime Prazo { get; set; }
    public Situacoes Situacao { get; set; }
    public Departamentos DepartamentoSolicitante { get; set; }
    public Usuario.Usuario UsuarioSolicitante { get; set; }
    public Departamentos DepartamentoResponsavel { get; set; }
    public Usuario.Usuario? UsuarioResponsavel { get; set; }
    public string Detalhes { get; set; } = string.Empty;
}
