using Microsoft.EntityFrameworkCore;
using TechChallenge.Dominio;

namespace TechChallenge.Infraestrutura.Repositorios;

public class EFRepositorio<T> : IRepositorio<T> where T : Entidade
{
    protected ApplicationDbContext _context;
    protected DbSet<T> _dbSet;

    public EFRepositorio(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Criar(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public IList<T> BuscarTodas()
    {
        return _dbSet.ToList();
    }

    public T? BuscarPorId(int id)
    {
        return _dbSet.FirstOrDefault(t => t.Id == id);
    }

    public void Editar(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Apagar(int id)
    {
        var entity = BuscarPorId(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
