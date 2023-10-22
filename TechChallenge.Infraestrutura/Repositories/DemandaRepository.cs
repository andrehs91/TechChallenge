using TechChallenge.Dominio.Demanda;
using TechChallenge.Infraestrutura.Data;

namespace TechChallenge.Infraestrutura.Repositories;

public class DemandaRepository : IDemandaRepository
{
    public ApplicationDbContext _context;
    public DemandaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Criar(Demanda demanda)
    {
        _context.Demandas.Add(demanda);
        _context.SaveChanges();
    }

    public IList<Demanda> BuscarTodas()
    {
        return _context.Demandas.ToList();
    }

    public Demanda? BuscarPorId(int id)
    {
        return _context.Demandas.Find(id);
    }

    public void Editar(Demanda demanda)
    {
        _context.Demandas.Update(demanda);
        _context.SaveChanges();
    }

    public void Apagar(Demanda demanda)
    {
        _context.Demandas.Remove(demanda);
        _context.SaveChanges();
    }
}
