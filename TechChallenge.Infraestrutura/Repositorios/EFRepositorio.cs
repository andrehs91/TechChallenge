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

    public void Create(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public IList<T> ReadAll()
    {
        return _dbSet.ToList();
    }

    public T? ReadById(int id)
    {
        return _dbSet.FirstOrDefault(t => t.Id == id);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = ReadById(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
