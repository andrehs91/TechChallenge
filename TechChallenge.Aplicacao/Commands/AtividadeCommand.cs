using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Exceptions;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Commands;

public class AtividadeCommand
{
    private readonly IAtividadeRepository _repositorio;
    private readonly AtividadeAgragacao _agregacao;

    public AtividadeCommand(IAtividadeRepository repositorio, AtividadeAgragacao agregacao)
    {
        _repositorio = repositorio;
        _agregacao = agregacao;
    }

    public Tuple<int, Atividade> CriarAtividade(Usuario usuario, AtividadeDTO atividadeDTO)
    {
        Atividade atividade = AtividadeDTO.DTOParaEntidade(atividadeDTO);
        
        if (!_agregacao.UsuarioPodeCriarAtividade(usuario, atividade))
            throw new UsuarioNaoAutorizadoException();
        
        _repositorio.CriarAtividade(atividade);
        return Tuple.Create(atividade.Id, atividade);
    }

    public IList<Atividade> BuscarAtividades()
    {
        return _repositorio.BuscarAtividades();
    }

    public Atividade? BuscarAtividade(int id)
    {
        return _repositorio.BuscarAtividade(id);
    }

    public bool EditarAtividade(Usuario usuario, AtividadeDTO atividadeDTO)
    {
        var atividade = _repositorio.BuscarAtividade(atividadeDTO.Id);

        if (atividade is null) return false;

        if (!_agregacao.UsuarioPodeEditarAtividade(usuario, atividade))
            throw new UsuarioNaoAutorizadoException();

        atividade.Nome = atividadeDTO.Nome;
        atividade.Descricao = atividadeDTO.Descricao;
        atividade.EstahAtiva = atividadeDTO.EstahAtiva;
        atividade.DepartamentoResponsavel = atividadeDTO.DepartamentoResponsavel;
        atividade.TipoDeDistribuicao = atividadeDTO.TipoDeDistribuicao;
        atividade.Prioridade = atividadeDTO.Prioridade;
        atividade.ContagemDePrazo = atividadeDTO.ContagemDePrazo;
        atividade.PrazoEstimado = atividadeDTO.PrazoEstimado;
        _repositorio.EditarAtividade(atividade);

        return true;
    }

    public bool ApagarAtividade(Usuario usuario, int id)
    {
        var atividade = _repositorio.BuscarAtividade(id);

        if (atividade is null) return false;

        if (!_agregacao.UsuarioPodeApagarAtividade(usuario, atividade))
            throw new UsuarioNaoAutorizadoException();

        _repositorio.ApagarAtividade(atividade);

        return true;
    }
}
