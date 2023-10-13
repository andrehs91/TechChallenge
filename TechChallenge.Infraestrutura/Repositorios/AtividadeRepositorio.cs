using TechChallenge.Dominio.Atividade;

namespace TechChallenge.Infraestrutura.Repositorios;

public class AtividadeRepositorio : EFRepositorio<Atividade>, IAtividadeRepositorio
{
    public AtividadeRepositorio(ApplicationDbContext context) : base(context)
    {
    }
}
