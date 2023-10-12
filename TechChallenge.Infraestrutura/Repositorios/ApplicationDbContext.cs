using Microsoft.EntityFrameworkCore;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Infraestrutura.Repositorios;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
}
