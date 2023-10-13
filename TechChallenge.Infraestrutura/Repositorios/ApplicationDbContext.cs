using Microsoft.EntityFrameworkCore;
using TechChallenge.Dominio.Usuario;
using TechChallenge.Dominio.Atividade;

namespace TechChallenge.Infraestrutura.Repositorios;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
    public DbSet<Atividade> Atividades { get; set; } = null!;
}
