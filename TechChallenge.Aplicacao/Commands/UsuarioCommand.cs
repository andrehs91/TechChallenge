using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Commands;

public class UsuarioCommand
{
    private readonly IUsuarioRepository _repositorioDeUsuarios;

    public UsuarioCommand(IUsuarioRepository repositorioDeUsuarios)
    {
        _repositorioDeUsuarios = repositorioDeUsuarios;
    }

    public Usuario? Autenticar(AutenticarDTO autenticarDTO)
    {
        if (autenticarDTO.Senha != "senha") return null;

        return _repositorioDeUsuarios.BuscarUsuarioPorMatricula(autenticarDTO.Matricula);
    }

    public RespostaDTO DefinirGestoresDoDepartamento(Usuario usuario, IdsDosUsuariosDTO idsDosUsuariosDTO)
    {
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
            usuarioPromovido.Funcao = Funcoes.Gestor;
        foreach (Usuario usuarioDemovivo in usuariosDemovivos)
            usuarioDemovivo.Funcao = Funcoes.Solicitante;
        var usuarios = usuariosPromovidos.Concat(usuariosDemovivos).ToList();

        _repositorioDeUsuarios.DefinirGestores(usuarios);
        return new RespostaDTO(RespostaDTO.Tipos.Sucesso, "Gestor(es) definido(s) com sucesso.");
    }

    public IList<Usuario> ListarUsuariosDoDepartamento(Usuario usuario)
    {
        return _repositorioDeUsuarios.BuscarUsuariosPorDepartamento(usuario.Departamento);
    }
}
