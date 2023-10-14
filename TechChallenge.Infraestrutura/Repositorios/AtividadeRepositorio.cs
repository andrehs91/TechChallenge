using Microsoft.EntityFrameworkCore;
using TechChallenge.Dominio.Atividade;

namespace TechChallenge.Infraestrutura.Repositorios;

public class AtividadeRepositorio : IAtividadeRepositorio
{
    public ApplicationDbContext _context;
    public AtividadeRepositorio(ApplicationDbContext context)
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

    public Atividade? BuscarAtividade(int id)
    {
        return _context.Atividades.Find(id);
    }

    public void EditarAtividade(Atividade atividade)
    {
        _context.Entry(atividade).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void ApagarAtividade(Atividade atividade)
    {
        _context.Atividades.Remove(atividade);
        _context.SaveChanges();
    }
}
