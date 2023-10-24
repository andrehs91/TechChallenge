using Microsoft.EntityFrameworkCore;
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
        return _context.Demandas
            .Include(d => d.EventosRegistrados)
            .Include(d => d.UsuarioSolicitante)
            .Include(d => d.UsuarioSolucionador)
            .Where(d => d.Id == id)
            .FirstOrDefault();
    }

    public IList<Demanda> BuscarTodas()
    {
        return _context.Demandas.AsNoTracking().ToList();
    }

    public IList<Demanda> BuscarPorSolicitante(int idSolicitante)
    {
        return _context.Demandas
            .Where(d => d.UsuarioSolicitante.Id == idSolicitante)
            .AsNoTracking()
            .ToList();
    }

    public IList<Demanda> BuscarPorDepartamentoSolicitante(Departamentos departamento)
    {
        return _context.Demandas
            .Where(d => d.DepartamentoSolicitante == departamento)
            .AsNoTracking()
            .ToList();
    }

    public IList<Demanda> BuscarPorSolucionador(int idSolucionador)
    {
        return _context.Demandas
            .Where(d => d.UsuarioSolucionador != null && d.UsuarioSolucionador.Id == idSolucionador)
            .AsNoTracking()
            .ToList();
    }

    public IList<Demanda> BuscarPorDepartamentoSolucionador(Departamentos departamento)
    {
        return _context.Demandas
            .Where(d => d.DepartamentoSolucionador == departamento)
            .AsNoTracking()
            .ToList();
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
