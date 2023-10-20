using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Demanda;
using TechChallenge.Dominio.Exceptions;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Commands;

public class DemandaCommand
{
    private readonly IDemandaRepository _repositorio;
    private readonly IAtividadeRepository _repositorioAtividade;
    private readonly DemandaAgragacao _agregacao;
    

    public DemandaCommand(IDemandaRepository repositorio, IAtividadeRepository repositorioAtividade, DemandaAgragacao agregacao)
    {
        _repositorio = repositorio;
        _agregacao = agregacao;
        _repositorioAtividade = repositorioAtividade;
    }

    public Tuple<long, Demanda> CriarDemanda(Usuario usuario, DemandaDTO demandaDTO)
    {
        Atividade? atividade = _repositorioAtividade.BuscarAtividade(demandaDTO.AtividadeId);
        if (atividade == null)
        {
            throw new AtividadeException("Atividade não encontrada com id " + demandaDTO.AtividadeId.ToString());
        }

        Demanda demanda = DemandaDTO.DTOParaEntidade(demandaDTO, atividade, usuario);
        demanda.Prazo = atividade.getPrazoAtividade();

        _repositorio.CriarDemanda(demanda);
        return Tuple.Create(demanda.NumeroDaDemanda, demanda);
    }

    public IList<Demanda> BuscarDemandas()
    {
        return _repositorio.BuscarDemandas();
    }

    public Demanda? BuscarDemanda(int id)
    {
        return _repositorio.BuscarDemanda(id);
    }

    public bool EditarDemanda(Usuario usuario, DemandaDTO demandaDTO)
    {
        var demanda = _repositorio.BuscarDemanda(demandaDTO.NumeroDaDemanda);

        Atividade? atividade = _repositorioAtividade.BuscarAtividade(demandaDTO.AtividadeId);

        if (demanda is null) return false;
        if (atividade is null) return false;

        if (!_agregacao.UsuarioPodeEditarDemanda(usuario, demanda))
            throw new UsuarioNaoAutorizadoException();

        demanda.atividade = atividade;
        demanda.NumeroDaDemandaReaberta = demandaDTO.NumeroDaDemandaReaberta;
        demanda.NumeroDaDemandaDesdobrada = demandaDTO.NumeroDaDemandaDesdobrada;
        demanda.MomentoDeFechamento = demandaDTO.MomentoDeFechamento;
        demanda.Prazo = demandaDTO.Prazo;
        demanda.Detalhes = demandaDTO.Detalhes;
        demanda.Situacao = demandaDTO.Situacao;
        demanda.DepartamentoResponsavel = demandaDTO.DepartamentoResponsavel;

        _repositorio.EditarDemanda(demanda);

        return true;
    }

    public bool FecharDemanda(Usuario usuario, int id)
    {
        var demanda = _repositorio.BuscarDemanda(id);

        if (demanda is null) return false;

        if (!_agregacao.UsuarioPodeEditarDemanda(usuario, demanda))
            throw new UsuarioNaoAutorizadoException();

        demanda.MomentoDeFechamento = DateTime.Now;

        _repositorio.EditarDemanda(demanda);

        return true;
    }
}
