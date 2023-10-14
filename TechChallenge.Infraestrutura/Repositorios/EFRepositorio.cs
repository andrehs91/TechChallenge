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

    public void Criar(T entidade)
    {
        _dbSet.Add(entidade);
        _context.SaveChanges();
    }

    public IList<T> BuscarEntidades()
    {
        return _dbSet.ToList();
    }

    public T? BuscarEntidade(int id)
    {
        return _dbSet.FirstOrDefault(t => t.Id == id);
    }

    public void Editar(T entidade)
    {
        _dbSet.Update(entidade);
        _context.SaveChanges();
    }

    public void Apagar(int id)
    {
        var entidade = BuscarEntidade(id);
        if (entidade != null)
        {
            _dbSet.Remove(entidade);
            _context.SaveChanges();
        }
    }
}
