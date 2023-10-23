using TechChallenge.Dominio.Demanda;
using TechChallenge.Dominio.Enums;
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

    public Demanda? BuscarPorId(int id)
    {
        return _context.Demandas.Find(id);
    }

    public IList<Demanda> BuscarTodas()
    {
        return _context.Demandas.ToList();
    }

    public IList<Demanda> BuscarPorSolicitante(int idSolicitante)
    {
        return _context.Demandas.Where(d => d.UsuarioSolicitante.Id == idSolicitante).ToList();
    }

    public IList<Demanda> BuscarPorDepartamentoSolicitante(Departamentos departamento)
    {
        return _context.Demandas.Where(d => d.DepartamentoSolicitante == departamento).ToList();
    }

    public IList<Demanda> BuscarPorResponsavel(int idResponsavel)
    {
        return _context.Demandas.Where(d => d.UsuarioResponsavel != null && d.UsuarioResponsavel.Id == idResponsavel).ToList();
    }

    public IList<Demanda> BuscarPorDepartamentoResponsavel(Departamentos departamento)
    {
        return _context.Demandas.Where(d => d.DepartamentoResponsavel == departamento).ToList();
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
