using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Demanda;
using TechChallenge.Dominio.Exceptions;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Commands;

public class DemandaCommand
{
    private readonly IAtividadeRepository _repositorioDeAtividades;
    private readonly IDemandaRepository _repositorioDeDemandas;
    private readonly IUsuarioRepository _repositorioDeUsuarios;

    public DemandaCommand(IAtividadeRepository repositorioDeAtividades, IDemandaRepository repositorioDeDemandas, IUsuarioRepository repositorioDeUsuarios)
    {
        _repositorioDeAtividades = repositorioDeAtividades;
        _repositorioDeDemandas = repositorioDeDemandas;
        _repositorioDeUsuarios = repositorioDeUsuarios;
    }

    public Demanda AbrirDemanda(Usuario ator, int idDaAtividade, string detalhes)
    {
        var atividade = _repositorioDeAtividades.BuscarAtividade(idDaAtividade);
        if (atividade is null) throw new AtividadeNaoEncontradaException();

        Demanda demanda = new( atividade, null, ator, detalhes );
        _repositorioDeDemandas.CriarDemanda(demanda);
        return demanda;
    }

    public void EncaminharDemanda(Usuario ator, long numeroDaDemanda, int idNovoResponsavel, string mensagem)
    {
        var demanda = _repositorioDeDemandas.BuscarDemanda(numeroDaDemanda);
        if (demanda is null) throw new DemandaNaoEncontradaException();

        var novoResponsavel = _repositorioDeUsuarios.BuscarEntidade(idNovoResponsavel);
        if (novoResponsavel is null) throw new UsuarioNaoEncontradoException();

        demanda.Encaminhar(ator, novoResponsavel, mensagem);
        _repositorioDeDemandas.EditarDemanda(demanda);
    }

    public void CapturarDemanda(Usuario ator, long numeroDaDemanda, string mensagem)
    {
        var demanda = _repositorioDeDemandas.BuscarDemanda(numeroDaDemanda);
        if (demanda is null) throw new DemandaNaoEncontradaException();

        demanda.Capturar(ator);
        _repositorioDeDemandas.EditarDemanda(demanda);
    }

    public void RejeitarDemanda(Usuario ator, long numeroDaDemanda, string mensagem)
    {
        var demanda = _repositorioDeDemandas.BuscarDemanda(numeroDaDemanda);
        if (demanda is null) throw new DemandaNaoEncontradaException();

        demanda.Rejeitar(ator, mensagem);
        _repositorioDeDemandas.EditarDemanda(demanda);
    }

    public void ResponderDemanda(Usuario ator, long numeroDaDemanda, string mensagem)
    {
        var demanda = _repositorioDeDemandas.BuscarDemanda(numeroDaDemanda);
        if (demanda is null) throw new DemandaNaoEncontradaException();

        demanda.Responder(ator, mensagem);
        _repositorioDeDemandas.EditarDemanda(demanda);
    }

    public void CancelarDemanda(Usuario ator, long numeroDaDemanda, string mensagem)
    {
        var demanda = _repositorioDeDemandas.BuscarDemanda(numeroDaDemanda);
        if (demanda is null) throw new DemandaNaoEncontradaException();

        demanda.Cancelar(ator, mensagem);
        _repositorioDeDemandas.EditarDemanda(demanda);
    }

    public Demanda ReabrirDemanda(Usuario ator, long numeroDaDemanda, string mensagem)
    {
        var demanda = _repositorioDeDemandas.BuscarDemanda(numeroDaDemanda);
        if (demanda is null) throw new DemandaNaoEncontradaException();

        Demanda novaDemanda = demanda.Reabrir(ator, mensagem);
        _repositorioDeDemandas.CriarDemanda(novaDemanda);
        return novaDemanda;
    }
}
