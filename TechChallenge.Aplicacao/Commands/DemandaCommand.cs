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

    public Demanda BuscarDemanda(int id)
    {
        return _repositorioDeDemandas.BuscarPorId(id) ?? throw new DemandaNaoEncontradaException();
    }

    public Demanda AbrirDemanda(Usuario ator, int id, string detalhes)
    {
        var atividade = _repositorioDeAtividades.BuscarPorId(id) ?? throw new AtividadeNaoEncontradaException();
        Demanda demanda = new( atividade, ator, detalhes );
        _repositorioDeDemandas.Criar(demanda);
        return demanda;
    }

    public void EncaminharDemanda(Usuario ator, int id, int idNovoResponsavel, string mensagem)
    {
        var novoResponsavel = _repositorioDeUsuarios.BuscarPorId(idNovoResponsavel) ?? throw new UsuarioNaoEncontradoException();
        var demanda = BuscarDemanda(id);
        demanda.Encaminhar(ator, novoResponsavel, mensagem);
        _repositorioDeDemandas.Editar(demanda);
    }

    public void CapturarDemanda(Usuario ator, int id)
    {
        var demanda = BuscarDemanda(id);
        demanda.Capturar(ator);
        _repositorioDeDemandas.Editar(demanda);
    }

    public void RejeitarDemanda(Usuario ator, int id, string mensagem)
    {
        var demanda = BuscarDemanda(id);
        demanda.Rejeitar(ator, mensagem);
        _repositorioDeDemandas.Editar(demanda);
    }

    public void ResponderDemanda(Usuario ator, int id, string mensagem)
    {
        var demanda = BuscarDemanda(id);
        demanda.Responder(ator, mensagem);
        _repositorioDeDemandas.Editar(demanda);
    }

    public void CancelarDemanda(Usuario ator, int id, string mensagem)
    {
        var demanda = BuscarDemanda(id);
        demanda.Cancelar(ator, mensagem);
        _repositorioDeDemandas.Editar(demanda);
    }

    public Demanda ReabrirDemanda(Usuario ator, int id, string mensagem)
    {
        var demanda = BuscarDemanda(id);
        Demanda novaDemanda = demanda.Reabrir(ator, mensagem);
        _repositorioDeDemandas.Editar(demanda);
        _repositorioDeDemandas.Criar(novaDemanda);
        return novaDemanda;
    }

    internal object? BuscarDemandasDoSolicitante(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    internal object? BuscarDemandasDoDepartamentoSolicitante(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    internal object? BuscarDemandasDoSolucionador(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    internal object? BuscarDemandasDoDepartamentoSolucionador(Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
