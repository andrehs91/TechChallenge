using TechChallenge.Dominio.Demanda;

namespace TechChallenge.Infraestrutura.Repositorios;

public class DemandaRepositorio : IDemandaRepositorio
{
    public ApplicationDbContext _context;
    public DemandaRepositorio(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CriarDemanda(Demanda demanda)
    {
        _context.Demandas.Add(demanda);
        _context.SaveChanges();
    }

    public IList<Demanda> BuscarDemandas()
    {
        return _context.Demandas.ToList();
    }

    public Demanda? BuscarDemanda(long numeroDaDemanda)
    {
        return _context.Demandas.Find(numeroDaDemanda);
    }

    public void EditarDemanda(Demanda demanda)
    {
        _context.Demandas.Update(demanda);
        _context.SaveChanges();
    }

    public void ApagarDemanda(Demanda demanda)
    {
        _context.Demandas.Remove(demanda);
        _context.SaveChanges();
    }
}
