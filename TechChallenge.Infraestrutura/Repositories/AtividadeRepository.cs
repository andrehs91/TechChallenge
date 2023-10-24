using Microsoft.EntityFrameworkCore;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Enums;
using TechChallenge.Infraestrutura.Data;

namespace TechChallenge.Infraestrutura.Repositories;

public class AtividadeRepository : IAtividadeRepository
{
    public ApplicationDbContext _context;
    public AtividadeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Criar(Atividade atividade)
    {
        _context.Atividades.Add(atividade);
        _context.SaveChanges();
    }

    public Atividade? BuscarPorId(int id)
    {
        return _context.Atividades.Include(a => a.Solucionadores).Where(a => a.Id == id).FirstOrDefault();
    }

    public IList<Atividade> BuscarTodas()
    {
        return _context.Atividades.ToList();
    }

    public IList<Atividade> BuscarAtivas()
    {
        return _context.Atividades.Where(a => a.EstahAtiva).ToList();
    }

    public IList<Atividade> BuscarPorDepartamentoSolucionador(Departamentos departamento)
    {
        return _context.Atividades.Where(a => a.DepartamentoSolucionador == departamento).ToList();
    }

    public void Editar(Atividade atividade)
    {
        _context.Atividades.Update(atividade);
        _context.SaveChanges();
    }

    public void Apagar(Atividade atividade)
    {
        _context.Atividades.Remove(atividade);
        _context.SaveChanges();
    }
}
