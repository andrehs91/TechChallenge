namespace TechChallenge.Dominio;

public interface IRepositorio<T> where T : Entidade
{
    void Create(T entity);
    IList<T> ReadAll();
    T? ReadById(int id);
    void Update(T entity);
    void Delete(int id);
}
