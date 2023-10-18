using Microsoft.EntityFrameworkCore;
using TechChallenge.Dominio.Usuario;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Demanda;
using TechChallenge.Dominio.EventoRegistrado;

namespace TechChallenge.Infraestrutura.Repositories;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
    public DbSet<Atividade> Atividades { get; set; } = null!;
    public DbSet<Demanda> Demandas { get; set; } = null!;
    public DbSet<EventoRegistrado> EventosRegistrados { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TechChallenge;Trusted_Connection=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema("App");

        modelBuilder.Entity<Atividade>(a =>
        {
            a.HasKey(a => a.Id);
            a.HasMany(a => a.Demandas)
                .WithOne(d => d.Atividade)
                .HasForeignKey(d => d.NumeroDaDemanda)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Demanda>(d =>
        {
            d.HasKey(d => d.NumeroDaDemanda);
            d.HasOne(d => d.Atividade)
                .WithMany(a => a.Demandas)
                .HasPrincipalKey(a => a.Id);
            d.HasMany(d => d.Historico)
                .WithOne(h => h.Demanda)
                .HasForeignKey(h => h.NumeroDoRegistro)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EventoRegistrado>(er =>
        {
            er.HasKey(er => er.NumeroDoRegistro);
            er.HasOne(er => er.Demanda)
                .WithMany(d => d.Historico)
                .HasPrincipalKey(d => d.NumeroDaDemanda);
        });
    }
}
