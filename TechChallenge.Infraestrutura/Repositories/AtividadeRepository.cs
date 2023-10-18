using TechChallenge.Dominio.Atividade;

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

    public Atividade? BuscarAtividade(int id)
    {
        return _context.Atividades.Find(id);
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
