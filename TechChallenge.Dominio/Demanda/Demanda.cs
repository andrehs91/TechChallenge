using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Exceptions;
using TechChallenge.Dominio.Policies;

namespace TechChallenge.Dominio.Demanda;

public class Demanda
{
    public int Id { get; set; }
    public int AtividadeId { get; set; }
    public Atividade.Atividade Atividade { get; set; } = null!;
    public virtual List<EventoRegistrado.EventoRegistrado> EventosRegistrados { get; set; } = new();
    public int? IdDaDemandaReaberta { get; set; }
    public DateTime MomentoDeAbertura { get; set; }
    public DateTime? MomentoDeFechamento { get; set; } = null;
    public DateTime Prazo { get; set; }
    public Situacoes Situacao { get; set; }
    public Departamentos DepartamentoSolicitante { get; set; }
    public Usuario.Usuario UsuarioSolicitante { get; set; }
    public Departamentos DepartamentoSolucionador { get; set; }
    public Usuario.Usuario? UsuarioSolucionador { get; set; }
    public string Detalhes { get; set; } = string.Empty;

    public Demanda() { }

    public Demanda(Atividade.Atividade atividade,
        Usuario.Usuario ator,
        string detalhes,
        int? idDaDemandaReaberta = null)
    {
        Situacoes situacao = Situacoes.AguardandoDistribuicao;
        Usuario.Usuario? usuarioSolucionador = IdentificarSolucionadorPolicy.IdentificarSolucionador(atividade);
        if (usuarioSolucionador is not null) situacao = Situacoes.EmAtendimento;

        Atividade = atividade;
        IdDaDemandaReaberta = idDaDemandaReaberta;
        MomentoDeAbertura = DateTime.Now;
        Prazo = DateTime.Now.AddMinutes(atividade.PrazoEstimado);
        Situacao = situacao;
        DepartamentoSolicitante = ator.Departamento;
        UsuarioSolicitante = ator;
        DepartamentoSolucionador = atividade.DepartamentoSolucionador;
        UsuarioSolucionador = usuarioSolucionador;
        Detalhes = detalhes;

        RegistrarEvento();
    }

    private void RegistrarEvento()
    {
        EventosRegistrados.Add(new EventoRegistrado.EventoRegistrado()
        {
            Demanda = this,
            UsuarioSolucionador = UsuarioSolucionador,
            Situacao = Situacao,
            MomentoInicial = DateTime.Now
        });
    }

    private void AlterarEventoRegistrado(string mensagem)
    {
        var ultimo = EventosRegistrados.OrderByDescending(er => er.Id).FirstOrDefault();
        int indice = EventosRegistrados.ToList().IndexOf(ultimo!);
        EventosRegistrados[indice].Situacao = Situacao;
        EventosRegistrados[indice].MomentoFinal = DateTime.Now;
        EventosRegistrados[indice].Mensagem = mensagem;
    }

    public void Encaminhar(Usuario.Usuario ator, Usuario.Usuario novoSolucionador, string mensagem)
    {
        if (UsuarioSolucionador is not null && UsuarioSolucionador.Id == ator.Id)
            Situacao = Situacoes.EncaminhadaPeloSolucionador;
        else if (DepartamentoSolucionador == ator.Departamento && ator.Funcao == Funcoes.Gestor)
            Situacao = Situacoes.EncaminhadaPeloGestor;
        else
            throw new UsuarioNaoAutorizadoException("O encaminhamento de demandas é restrito ao solucionador da demanda e aos gestores do departamento solucionador.");

        AlterarEventoRegistrado(mensagem);

        UsuarioSolucionador = novoSolucionador;
        Situacao = Situacoes.EmAtendimento;
        RegistrarEvento();
    }

    public void Capturar(Usuario.Usuario ator)
    {
        if (DepartamentoSolucionador != ator.Departamento)
            throw new UsuarioNaoAutorizadoException("A captura de demandas é restrita aos usuários do departamento solucionador.");

        Situacao = Situacoes.Capturada;
        AlterarEventoRegistrado($"Demanda capturada por {ator.Nome}.");

        UsuarioSolucionador = ator;
        Situacao = Situacoes.EmAtendimento;
        RegistrarEvento();
    }

    public void Rejeitar(Usuario.Usuario ator, string mensagem)
    {
        if (UsuarioSolucionador is null || UsuarioSolucionador.Id != ator.Id)
            throw new UsuarioNaoAutorizadoException("A rejeição de demandas é restrita ao solucionador da demanda.");

        Situacao = Situacoes.Rejeitada;
        AlterarEventoRegistrado(mensagem);

        UsuarioSolucionador = null;
        Situacao = Situacoes.AguardandoDistribuicao;
        RegistrarEvento();
    }

    public void Responder(Usuario.Usuario ator, string mensagem)
    {
        if (UsuarioSolucionador is null || UsuarioSolucionador.Id != ator.Id)
            throw new UsuarioNaoAutorizadoException("Apenas o solucionador da demanda pode respondê-la.");

        MomentoDeFechamento = DateTime.Now;
        Situacao = Situacoes.Respondida;
        AlterarEventoRegistrado(mensagem);
    }

    public void Cancelar(Usuario.Usuario ator, string mensagem)
    {
        if (UsuarioSolicitante.Id == ator.Id)
            Situacao = Situacoes.CanceladaPeloSolicitante;
        else if (UsuarioSolucionador is not null && UsuarioSolucionador.Id == ator.Id)
            Situacao = Situacoes.CanceladaPeloSolucionador;
        else if (DepartamentoSolucionador == ator.Departamento && ator.Funcao == Funcoes.Gestor)
            Situacao = Situacoes.CanceladaPeloGestor;
        else
            throw new UsuarioNaoAutorizadoException("O cancelamento de demandas é restrito ao solicitante da demanda, ao solucionador da demanda e aos gestores do departamento solucionador.");

        MomentoDeFechamento = DateTime.Now;
        AlterarEventoRegistrado(mensagem);
    }

    public Demanda Reabrir(Usuario.Usuario ator, string mensagem)
    {
        if (DepartamentoSolicitante != ator.Departamento)
            throw new UsuarioNaoAutorizadoException("A reabertura de demandas é restrita aos usuários do departamento solicitante.");

        string detalhes = $"Esta demanda é a reabertura da demanda {Id}. Motivo da reabertura: {mensagem}. Detalhes da demanda reaberta: {Detalhes}.";
        return new Demanda(Atividade, ator, detalhes, Id);
    }
}
