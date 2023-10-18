namespace TechChallenge.Dominio.Usuario;

public interface IUsuarioRepository : IEntidadeRepository<Usuario>
{
    public Usuario? BuscarPorMatricula(string matricula);
}
