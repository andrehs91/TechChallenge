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

    public Demanda AbrirDemanda(Usuario ator, int id, string detalhes)
    {
        var atividade = _repositorioDeAtividades.BuscarPorId(id) ?? throw new AtividadeNaoEncontradaException();
        Demanda demanda = new( atividade, ator, detalhes );
        _repositorioDeDemandas.Criar(demanda);
        return demanda;
    }

    public Demanda ConsultarDemanda(int id)
    {
        return _repositorioDeDemandas.BuscarPorId(id) ?? throw new DemandaNaoEncontradaException();
    }

    internal IList<Demanda> ListarDemandasDoSolicitante(Usuario usuario)
    {
        return _repositorioDeDemandas.BuscarPorSolicitante(usuario.Id);
    }

    internal IList<Demanda> ListarDemandasDoDepartamentoSolicitante(Usuario usuario)
    {
        return _repositorioDeDemandas.BuscarPorDepartamentoSolicitante(usuario.Departamento);
    }

    internal IList<Demanda> ListarDemandasDoResponsavel(Usuario usuario)
    {
        return _repositorioDeDemandas.BuscarPorResponsavel(usuario.Id);
    }

    internal IList<Demanda> ListarDemandasDoDepartamentoResponsavel(Usuario usuario)
    {
        return _repositorioDeDemandas.BuscarPorDepartamentoResponsavel(usuario.Departamento);
    }

    public void EncaminharDemanda(Usuario ator, int id, int idNovoResponsavel, string mensagem)
    {
        var novoResponsavel = _repositorioDeUsuarios.BuscarPorId(idNovoResponsavel) ?? throw new UsuarioNaoEncontradoException();
        var demanda = ConsultarDemanda(id);
        demanda.Encaminhar(ator, novoResponsavel, mensagem);
        _repositorioDeDemandas.Editar(demanda);
    }

    public void CapturarDemanda(Usuario ator, int id)
    {
        var demanda = ConsultarDemanda(id);
        demanda.Capturar(ator);
        _repositorioDeDemandas.Editar(demanda);
    }

    public void RejeitarDemanda(Usuario ator, int id, string mensagem)
    {
        var demanda = ConsultarDemanda(id);
        demanda.Rejeitar(ator, mensagem);
        _repositorioDeDemandas.Editar(demanda);
    }

    public void ResponderDemanda(Usuario ator, int id, string mensagem)
    {
        var demanda = ConsultarDemanda(id);
        demanda.Responder(ator, mensagem);
        _repositorioDeDemandas.Editar(demanda);
    }

    public void CancelarDemanda(Usuario ator, int id, string mensagem)
    {
        var demanda = ConsultarDemanda(id);
        demanda.Cancelar(ator, mensagem);
        _repositorioDeDemandas.Editar(demanda);
    }

    public Demanda ReabrirDemanda(Usuario ator, int id, string mensagem)
    {
        var demanda = ConsultarDemanda(id);
        Demanda novaDemanda = demanda.Reabrir(ator, mensagem);
        _repositorioDeDemandas.Editar(demanda);
        _repositorioDeDemandas.Criar(novaDemanda);
        return novaDemanda;
    }
}
