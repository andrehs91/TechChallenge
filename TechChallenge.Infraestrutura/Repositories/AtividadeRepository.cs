using Microsoft.EntityFrameworkCore;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Infraestrutura.Repositories;

public class AtividadeRepository : IAtividadeRepository
{
    public ApplicationDbContext _context;
    public AtividadeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CriarAtividade(Atividade atividade)
    {
        _context.Atividades.Add(atividade);
        _context.SaveChanges();
    }

    public IList<Atividade> BuscarAtividades()
    {
        return _context.Atividades.ToList();
    }

    public IList<Atividade> BuscarAtividadesAtivas()
    {
        return _context.Atividades.Where(a => a.EstahAtiva).ToList();
    }

    public IList<Atividade> BuscarAtividadesPorDepartamentoResponsavel(Departamentos departamento)
    {
        return _context.Atividades.Where(a => a.DepartamentoResponsavel == departamento).ToList();
    }

    public Atividade? BuscarAtividade(int id)
    {
        return _context.Atividades.Include(a => a.Solucionadores).Where(a => a.Id == id).FirstOrDefault();
    }

    public void EditarAtividade(Atividade atividade)
    {
        _context.Atividades.Update(atividade);
        _context.SaveChanges();
    }

    public void ApagarAtividade(Atividade atividade)
    {
        _context.Atividades.Remove(atividade);
        _context.SaveChanges();
    }
}
