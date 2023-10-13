namespace TechChallenge.Dominio;

public interface IRepositorio<T> where T : Entidade
{
    void Criar(T entity);
    IList<T> BuscarTodas();
    T? BuscarPorId(int id);
    void Editar(T entity);
    void Apagar(int id);
}
