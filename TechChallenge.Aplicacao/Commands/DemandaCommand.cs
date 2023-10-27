using TechChallenge.Dominio.Entities;
using TechChallenge.Dominio.Exceptions;
using TechChallenge.Dominio.Interfaces;

namespace TechChallenge.Aplicacao.Commands;

public class DemandaCommand
{
    private readonly IAtividadeRepository _repositorioDeAtividades;
    private readonly IDemandaRepository _repositorioDeDemandas;
    private readonly IUsuarioRepository _repositorioDeUsuarios;
    private readonly ISolucionadorPolicy _politicaSobreSolucionador;

    public DemandaCommand(IAtividadeRepository repositorioDeAtividades, IDemandaRepository repositorioDeDemandas, IUsuarioRepository repositorioDeUsuarios, ISolucionadorPolicy politicaSobreSolucionador)
    {
        _repositorioDeAtividades = repositorioDeAtividades;
        _repositorioDeDemandas = repositorioDeDemandas;
        _repositorioDeUsuarios = repositorioDeUsuarios;
        _politicaSobreSolucionador = politicaSobreSolucionador;
    }

    public Demanda AbrirDemanda(Usuario ator, int id, string detalhes)
    {
        var atividade = _repositorioDeAtividades.BuscarPorId(id) ?? throw new EntidadeNaoEncontradaException("Atividade não encontrada.");
        var solucionador = _politicaSobreSolucionador.IdentificarSolucionadorMenosAtarefado(atividade);
        Demanda demanda = new( atividade, ator, solucionador, detalhes);
        _repositorioDeDemandas.Criar(demanda);
        return _repositorioDeDemandas.BuscarPorId(demanda.Id)!;
    }

    public Demanda ConsultarDemanda(int id)
    {
        return _repositorioDeDemandas.BuscarPorId(id) ?? throw new EntidadeNaoEncontradaException("Demanda não encontrada.");
    }

    internal IList<Demanda> ListarDemandasDoSolicitante(Usuario usuario)
    {
        return _repositorioDeDemandas.BuscarPorSolicitante(usuario.Id);
    }

    internal IList<Demanda> ListarDemandasDoDepartamentoSolicitante(Usuario usuario)
    {
        return _repositorioDeDemandas.BuscarPorDepartamentoSolicitante(usuario.Departamento);
    }

    internal IList<Demanda> ListarDemandasDoSolucionador(Usuario usuario)
    {
        return _repositorioDeDemandas.BuscarPorSolucionador(usuario.Id);
    }

    internal IList<Demanda> ListarDemandasDoDepartamentoSolucionador(Usuario usuario)
    {
        return _repositorioDeDemandas.BuscarPorDepartamentoSolucionador(usuario.Departamento);
    }

    public void EncaminharDemanda(Usuario ator, int id, int idNovoSolucionador, string mensagem)
    {
        var novoSolucionador = _repositorioDeUsuarios.BuscarPorId(idNovoSolucionador) ?? throw new EntidadeNaoEncontradaException("Usuário não encontrado.");
        var demanda = ConsultarDemanda(id);
        demanda.Encaminhar(ator, novoSolucionador, mensagem);
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
        var solucionador = _politicaSobreSolucionador.IdentificarSolucionadorMenosAtarefado(demanda.Atividade);
        Demanda novaDemanda = demanda.Reabrir(ator, solucionador, mensagem);
        _repositorioDeDemandas.Criar(novaDemanda);
        return novaDemanda;
    }
}
