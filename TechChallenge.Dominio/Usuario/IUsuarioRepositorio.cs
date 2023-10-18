namespace TechChallenge.Dominio.Usuario;

public interface IUsuarioRepositorio : IRepositorio<Usuario>
{
    public Usuario? BuscarPorMatricula(string matricula);
}
