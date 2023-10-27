using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Exceptions;

namespace TechChallenge.Dominio.Entities;

public class Demanda
{
    public int Id { get; set; }
    public int AtividadeId { get; set; }
    public Atividade Atividade { get; set; } = null!;
    public virtual List<EventoRegistrado> EventosRegistrados { get; set; } = new();
    public int? IdDaDemandaReaberta { get; set; }
    public DateTime MomentoDeAbertura { get; set; }
    public DateTime? MomentoDeFechamento { get; set; } = null;
    public DateTime Prazo { get; set; }
    public Situacoes Situacao { get; set; }
    public string DepartamentoSolicitante { get; set; }
    public int UsuarioSolicitanteId { get; set; }
    public Usuario UsuarioSolicitante { get; set; }
    public string DepartamentoSolucionador { get; set; }
    public int? UsuarioSolucionadorId { get; set; }
    public Usuario? UsuarioSolucionador { get; set; }
    public string Detalhes { get; set; } = string.Empty;

    public Demanda() { }

    public Demanda(
        Atividade atividade,
        Usuario ator,
        Usuario? solucionador,
        string detalhes,
        int? idDaDemandaReaberta = null)
    {
        Situacoes situacao = Situacoes.AguardandoDistribuicao;
        if (solucionador is not null) situacao = Situacoes.EmAtendimento;

        Atividade = atividade;
        IdDaDemandaReaberta = idDaDemandaReaberta;
        MomentoDeAbertura = DateTime.Now;
        Prazo = DateTime.Now.AddMinutes(atividade.PrazoEstimado);
        Situacao = situacao;
        DepartamentoSolicitante = ator.Departamento;
        UsuarioSolicitanteId = ator.Id;
        DepartamentoSolucionador = atividade.DepartamentoSolucionador;
        UsuarioSolucionadorId = solucionador?.Id;
        Detalhes = detalhes;

        RegistrarEvento(solucionador);
    }

    private void RegistrarEvento(Usuario? solucionador = null)
    {
        EventosRegistrados.Add(new EventoRegistrado()
        {
            Demanda = this,
            UsuarioSolucionador = solucionador ?? UsuarioSolucionador,
            Situacao = Situacao,
            MomentoInicial = DateTime.Now
        });
    }

    private void AtualizarEventoRegistrado(string mensagem)
    {
        var ultimo = EventosRegistrados.OrderByDescending(er => er.Id).FirstOrDefault();
        int indice = EventosRegistrados.ToList().IndexOf(ultimo!);
        EventosRegistrados[indice].Situacao = Situacao;
        EventosRegistrados[indice].MomentoFinal = DateTime.Now;
        EventosRegistrados[indice].Mensagem = mensagem;
    }

    private void VerificarSeADemandaEstahAtiva()
    {
        if (Situacao != Situacoes.AguardandoDistribuicao && Situacao != Situacoes.EmAtendimento)
            throw new UsuarioNaoAutorizadoException("Esta demanda não está na situação 'Aguardando Distribuição' e nem na situação 'Em Atendimento'.");
    }

    public void Encaminhar(Usuario ator, Usuario novoSolucionador, string mensagem)
    {
        VerificarSeADemandaEstahAtiva();
        if (UsuarioSolucionador is not null && UsuarioSolucionador.Id == ator.Id)
        {
            if (ator.Id == novoSolucionador.Id) throw new UsuarioNaoAutorizadoException("O usuário já é o solucionador desta demanda.");
            Situacao = Situacoes.EncaminhadaPeloSolucionador;
        }
        else if (DepartamentoSolucionador == ator.Departamento && ator.EhGestor == true)
            Situacao = Situacoes.EncaminhadaPeloGestor;
        else
            throw new UsuarioNaoAutorizadoException("O encaminhamento de demandas é restrito ao solucionador da demanda e aos gestores do departamento solucionador.");

        string complemento = $"Demanda encaminhada para ({novoSolucionador.Matricula}) {novoSolucionador.Nome}.";
        if (Situacao == Situacoes.EncaminhadaPeloGestor) complemento += $" Gestor responsável pela ação: ({ator.Matricula}) {ator.Nome}. ";
        AtualizarEventoRegistrado(complemento + mensagem);

        UsuarioSolucionador = novoSolucionador;
        Situacao = Situacoes.EmAtendimento;
        RegistrarEvento();
    }

    public void Capturar(Usuario ator)
    {
        VerificarSeADemandaEstahAtiva();
        if (UsuarioSolucionador is not null && UsuarioSolucionador.Id == ator.Id)
            throw new UsuarioNaoAutorizadoException("O usuário já é o solucionador desta demanda.");
        if (DepartamentoSolucionador != ator.Departamento)
            throw new UsuarioNaoAutorizadoException("A captura de demandas é restrita aos usuários do departamento solucionador.");

        Situacao = Situacoes.Capturada;
        AtualizarEventoRegistrado($"Demanda capturada por ({ator.Matricula}) {ator.Nome}.");

        UsuarioSolucionador = ator;
        Situacao = Situacoes.EmAtendimento;
        RegistrarEvento();
    }

    public void Rejeitar(Usuario ator, string mensagem)
    {
        VerificarSeADemandaEstahAtiva();
        if (UsuarioSolucionador is null || UsuarioSolucionador.Id != ator.Id)
            throw new UsuarioNaoAutorizadoException("A rejeição de demandas é restrita ao solucionador da demanda.");

        Situacao = Situacoes.Rejeitada;
        AtualizarEventoRegistrado(mensagem);

        UsuarioSolucionador = null;
        Situacao = Situacoes.AguardandoDistribuicao;
        RegistrarEvento();
    }

    public void Responder(Usuario ator, string mensagem)
    {
        if (Situacao != Situacoes.EmAtendimento)
            throw new UsuarioNaoAutorizadoException("Esta demanda não está na situação 'Em Atendimento'.");
        if (UsuarioSolucionador is null || UsuarioSolucionador.Id != ator.Id)
            throw new UsuarioNaoAutorizadoException("Apenas o solucionador da demanda pode respondê-la.");

        MomentoDeFechamento = DateTime.Now;
        Situacao = Situacoes.Respondida;
        AtualizarEventoRegistrado(mensagem);
    }

    public void Cancelar(Usuario ator, string mensagem)
    {
        VerificarSeADemandaEstahAtiva();
        if (UsuarioSolicitante.Id == ator.Id)
            Situacao = Situacoes.CanceladaPeloSolicitante;
        else if (UsuarioSolucionador is not null && UsuarioSolucionador.Id == ator.Id)
            Situacao = Situacoes.CanceladaPeloSolucionador;
        else if (DepartamentoSolucionador == ator.Departamento && ator.EhGestor == true)
            Situacao = Situacoes.CanceladaPeloGestor;
        else
            throw new UsuarioNaoAutorizadoException("O cancelamento de demandas é restrito ao solicitante da demanda, ao solucionador da demanda e aos gestores do departamento solucionador.");

        MomentoDeFechamento = DateTime.Now;
        AtualizarEventoRegistrado(mensagem);
    }

    public Demanda Reabrir(Usuario ator, Usuario? solucionador, string mensagem)
    {
        if (Situacao == Situacoes.CanceladaPeloSolicitante)
            throw new UsuarioNaoAutorizadoException("Esta demanda não pode ser reaberta.");
        if (Situacao != Situacoes.Respondida && Situacao != Situacoes.CanceladaPeloSolucionador && Situacao != Situacoes.CanceladaPeloGestor)
            throw new UsuarioNaoAutorizadoException("Esta demanda ainda não foi respondida ou cancelada.");
        if (DepartamentoSolicitante != ator.Departamento)
            throw new UsuarioNaoAutorizadoException("A reabertura de demandas é restrita aos usuários do departamento solicitante.");

        string detalhes = $"Esta demanda é a reabertura da demanda {Id}. Motivo da reabertura: {mensagem}. Detalhes da demanda reaberta: {Detalhes}.";
        return new Demanda(Atividade, ator, solucionador, detalhes, Id);
    }
}
