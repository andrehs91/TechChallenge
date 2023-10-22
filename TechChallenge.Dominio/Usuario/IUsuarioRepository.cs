using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Usuario;

public interface IUsuarioRepository
{
    void Criar(Usuario usuario);
    Usuario? BuscarPorId(int id);
    Usuario? BuscarPorMatricula(string matricula);
    IList<Usuario> BuscarTodos();
    IList<Usuario> BuscarPorIds(IList<int> ids);
    IList<Usuario> BuscarPorMatriculas(IList<string> ids);
    IList<Usuario> BuscarPorDepartamento(Departamentos departamento);
    void Editar(Usuario usuario);
    void EditarVarios(IList<Usuario> usuarios);
    void Apagar(int id);
    void ApagarVarios(IList<int> ids);
}
