using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Exceptions;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Commands;

public class AtividadeCommand
{
    private readonly IAtividadeRepository _repositorioDeAtividades;
    private readonly IUsuarioRepository _repositorioDeUsuarios;

    public AtividadeCommand(IAtividadeRepository repositorioDeAtividades, IUsuarioRepository repositorioDeUsuarios)
    {
        _repositorioDeAtividades = repositorioDeAtividades;
        _repositorioDeUsuarios = repositorioDeUsuarios;
    }

    private void VerificarSeUsuarioEstahAutorizado(Usuario usuario, Atividade atividade)
    {
        if (usuario.Funcao != Funcoes.Gestor ||
            usuario.Departamento != atividade.DepartamentoResponsavel)
            throw new UsuarioNaoAutorizadoException();
    }

    public Tuple<int, Atividade> CriarAtividade(Usuario usuario, AtividadeDTO atividadeDTO)
    {
        Atividade atividade = AtividadeDTO.DTOParaEntidade(atividadeDTO);

        VerificarSeUsuarioEstahAutorizado(usuario, atividade);

        _repositorioDeAtividades.CriarAtividade(atividade);
        return Tuple.Create(atividade.Id, atividade);
    }

    public IList<Atividade> ListarAtividades()
    {
        return _repositorioDeAtividades.BuscarAtividades();
    }

    public IList<Atividade> ListarAtividadesAtivas()
    {
        return _repositorioDeAtividades.BuscarAtividadesAtivas();
    }

    public IList<Atividade> ListarAtividadesPorDepartamentoResponsavel(Usuario usuario)
    {
        return _repositorioDeAtividades.BuscarAtividadesPorDepartamentoResponsavel(usuario.Departamento);
    }

    public Atividade? ConsultarAtividade(int idDaAtividade)
    {
        return _repositorioDeAtividades.BuscarAtividade(idDaAtividade);
    }

    public bool EditarAtividade(Usuario usuario, AtividadeDTO atividadeDTO)
    {
        var atividade = _repositorioDeAtividades.BuscarAtividade(atividadeDTO.Id);

        if (atividade is null) return false;

        VerificarSeUsuarioEstahAutorizado(usuario, atividade);

        atividade.Nome = atividadeDTO.Nome;
        atividade.Descricao = atividadeDTO.Descricao;
        atividade.EstahAtiva = atividadeDTO.EstahAtiva;
        atividade.DepartamentoResponsavel = atividadeDTO.DepartamentoResponsavel;
        atividade.TipoDeDistribuicao = atividadeDTO.TipoDeDistribuicao;
        atividade.Prioridade = atividadeDTO.Prioridade;
        atividade.ContagemDePrazo = atividadeDTO.ContagemDePrazo;
        atividade.PrazoEstimado = atividadeDTO.PrazoEstimado;

        _repositorioDeAtividades.EditarAtividade(atividade);
        return true;
    }

    public bool ApagarAtividade(Usuario usuario, int idDaAtividade)
    {
        var atividade = _repositorioDeAtividades.BuscarAtividade(idDaAtividade);

        if (atividade is null) return false;

        VerificarSeUsuarioEstahAutorizado(usuario, atividade);

        _repositorioDeAtividades.ApagarAtividade(atividade);
        return true;
    }

    public RespostaDTO DefinirSolucionadores(Usuario usuario, IdsDosUsuariosDTO idsDosUsuariosDTO, int idDaAtividade)
    {
        var atividade = _repositorioDeAtividades.BuscarAtividade(idDaAtividade);

        if (atividade is null) return new RespostaDTO(RespostaDTO.Tipos.Aviso, "Atividade não encontrada.");
        if (atividade.DepartamentoResponsavel != usuario.Departamento) return new RespostaDTO(RespostaDTO.Tipos.Erro, "A atividade não é de responsabilidade do teu departamento.");

        var usuariosPromovidos = _repositorioDeUsuarios.BuscarUsuariosPorIds(idsDosUsuariosDTO.IdsDosUsuariosASeremPromovidos);
        var usuariosDemovivos = _repositorioDeUsuarios.BuscarUsuariosPorIds(idsDosUsuariosDTO.IdsDosUsuariosASeremDemovidos);

        int quantidadeDeIds = idsDosUsuariosDTO.IdsDosUsuariosASeremPromovidos.Count() + idsDosUsuariosDTO.IdsDosUsuariosASeremDemovidos.Count();
        int quantidadeDeUsuarios = usuariosPromovidos.Count() + usuariosDemovivos.Count();

        if (quantidadeDeUsuarios == 0)
            return new RespostaDTO(RespostaDTO.Tipos.Aviso, "Nenhum usuário foi encontrado.");
        if (quantidadeDeIds != quantidadeDeUsuarios)
            return new RespostaDTO(RespostaDTO.Tipos.Aviso, "Um ou mais usuários não foram encontrados.");
        if (usuariosPromovidos.Concat(usuariosDemovivos).Where(u => u.Departamento != usuario.Departamento).Count() > 0)
            return new RespostaDTO(RespostaDTO.Tipos.Erro, "Um ou mais usuários não fazem parte do teu departamento.");

        foreach (Usuario usuarioPromovido in usuariosPromovidos)
        {
            if (!atividade.Solucionadores.Contains(usuarioPromovido))
                atividade.Solucionadores.Add(usuarioPromovido);
        }
        foreach (Usuario usuarioDemovivo in usuariosDemovivos)
        {
            if (atividade.Solucionadores.Contains(usuarioDemovivo))
                atividade.Solucionadores.Remove(usuarioDemovivo);
        }

        _repositorioDeAtividades.EditarAtividade(atividade);
        return new RespostaDTO(RespostaDTO.Tipos.Sucesso, "Solucionador(es) definido(s) com sucesso.");
    }
}
