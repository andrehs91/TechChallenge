using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Usuario;

public interface IUsuarioRepository : IEntidadeRepository<Usuario>
{
    Usuario? BuscarUsuarioPorMatricula(string matricula);
    IList<Usuario> BuscarUsuariosPorIds(IList<long> idsDosUsuarios);
    IList<Usuario> BuscarUsuariosPorDepartamento(Departamentos departamento);
    void DefinirGestores(IList<Usuario> usuarios);
}
