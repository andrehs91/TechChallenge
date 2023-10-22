using System.ComponentModel.DataAnnotations;
using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Policies;

namespace TechChallenge.Dominio.Demanda;

public class Demanda
{
    [Key]
    public int Id { get; set; }
    public int AtividadeId { get; set; }
    public Atividade.Atividade Atividade { get; set; } = null!;
    public virtual ICollection<EventoRegistrado.EventoRegistrado> EventosRegistrados { get; set; } = new List<EventoRegistrado.EventoRegistrado>();
    public int? IdDaDemandaReaberta { get; set; }
    public DateTime MomentoDeAbertura { get; set; }
    public DateTime? MomentoDeFechamento { get; set; } = null;
    public DateTime Prazo { get; set; }
    public Situacoes Situacao { get; set; }
    public Departamentos DepartamentoSolicitante { get; set; }
    public Usuario.Usuario UsuarioSolicitante { get; set; }
    public Departamentos DepartamentoResponsavel { get; set; }
    public Usuario.Usuario? UsuarioResponsavel { get; set; }
    public string Detalhes { get; set; } = string.Empty;

    public Demanda() { }

    public Demanda(Atividade.Atividade atividade,
        Usuario.Usuario usuarioSolicitante,
        string detalhes,
        int? numeroDaDemandaReaberta = null)
    {
        Situacoes situacao = Situacoes.AguardandoDistribuicao;
        Usuario.Usuario? usuario = IdentificarSolucionadorPolicy.IdentificarSolucionador(atividade);
        if (usuario is not null) situacao = Situacoes.EmAtendimento;

        Atividade = atividade;
        IdDaDemandaReaberta = numeroDaDemandaReaberta;
        MomentoDeAbertura = DateTime.Now;
        Prazo = DateTime.Now.AddMinutes(atividade.PrazoEstimado);
        Situacao = situacao;
        DepartamentoSolicitante = usuarioSolicitante.Departamento;
        UsuarioSolicitante = usuarioSolicitante;
        DepartamentoResponsavel = atividade.DepartamentoResponsavel;
        UsuarioResponsavel = usuario;
        Detalhes = detalhes;

        // Registrar evento
    }

    private int? IdentificarUltimoEventoRegistrado()
    {
        if (EventosRegistrados is null) return null;
        var ultimoEventoRegistrado = EventosRegistrados.OrderByDescending(er => er.Id).FirstOrDefault();
        return EventosRegistrados.ToList().IndexOf(ultimoEventoRegistrado!);
    }

    public void Encaminhar(Usuario.Usuario ator, Usuario.Usuario novoResponsavel, string mensagem)
    {
        // Verificar:
        // - Se o ator é o resposável
        // - Ou, se o ator é o gestor do departamento responsável

        // Verificar:
        // - Se o novoResponsavel é um solucionador da atividade

        // Registrar evento
    }

    public void Capturar(Usuario.Usuario ator)
    {
        // Verificar:
        // - Se o ator pertence ao departamento responsável

        // Registrar evento
    }

    public void Rejeitar(Usuario.Usuario ator, string mensagem)
    {
        // Verificar:
        // - Se o ator é o resposável

        // Registrar evento
    }

    public void Responder(Usuario.Usuario ator, string mensagem)
    {
        // Verificar:
        // - Se o ator é o resposável
        
        // Registrar evento
    }

    public void Cancelar(Usuario.Usuario ator, string mensagem)
    {
        // Verificar:
        // - Se o ator é o solicitante
        // - Ou, se o ator é o resposável
        // - Ou, se o ator é o gestor do departamento responsável
        
        // Registrar evento
    }

    public Demanda Reabrir(Usuario.Usuario ator, string mensagem)
    {
        // Verificar:
        // - Se o usuário é o solicitante
        // - Ou, se o usuário pertence ao departamento solicitante

        // Registrar evento nas duas demandas
        throw new NotImplementedException();
    }
}
