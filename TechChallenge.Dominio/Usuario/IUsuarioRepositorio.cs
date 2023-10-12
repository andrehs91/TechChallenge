namespace TechChallenge.Dominio.Usuario;

public interface IUsuarioRepositorio : IRepositorio<Usuario>
{
    public Usuario? BuscarPorCodigoESenha(string matricula, string senha);
}
