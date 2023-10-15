using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Excecoes;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Comandos;

public class AtividadeComandos
{
    private readonly IAtividadeRepositorio _repositorio;
    private readonly AtividadeAgragacao _agregacao;

    public AtividadeComandos(IAtividadeRepositorio repositorio, AtividadeAgragacao agregacao)
    {
        _repositorio = repositorio;
        _agregacao = agregacao;
    }

    public Tuple<int, Atividade> CriarAtividade(Usuario usuario, AtividadeDTO atividadeDTO)
    {
        Atividade atividade = AtividadeDTO.DTOParaEntidade(atividadeDTO);
        
        if (!_agregacao.UsuarioPodeCriarAtividade(usuario, atividade))
            throw new UsuarioNaoAutorizadoExcecao();
        
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
            throw new UsuarioNaoAutorizadoExcecao();

        atividade.Nome = atividadeDTO.Nome;
        atividade.Descricao = atividadeDTO.Descricao;
        atividade.EstahAtiva = atividadeDTO.EstahAtiva;
        atividade.CodigoDoDepartamentoResponsavel = atividadeDTO.CodigoDoDepartamentoResponsavel;
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
            throw new UsuarioNaoAutorizadoExcecao();

        _repositorio.ApagarAtividade(atividade);

        return true;
    }
}
