﻿using Microsoft.EntityFrameworkCore;
using TechChallenge.Dominio.Usuario;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Demanda;
using TechChallenge.Dominio.EventoRegistrado;
using Microsoft.Extensions.Configuration;

namespace TechChallenge.Infraestrutura.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
    public DbSet<Atividade> Atividades { get; set; } = null!;
    public DbSet<Demanda> Demandas { get; set; } = null!;
    public DbSet<EventoRegistrado> EventosRegistrados { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SQLServer"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("App");
        
        modelBuilder.Entity<Atividade>()
            .HasMany(atividade => atividade.Solucionadores)
            .WithMany(usuario => usuario.Atividades)
            .UsingEntity("Solucionadores");

        modelBuilder.Entity<Atividade>()
            .HasMany(atividade => atividade.Demandas)
            .WithOne(demanda => demanda.Atividade)
            .HasForeignKey(demanda => demanda.AtividadeId)
            .IsRequired();

        modelBuilder.Entity<Demanda>()
            .HasMany(demanda => demanda.EventosRegistrados)
            .WithOne(eventoRegistrado => eventoRegistrado.Demanda)
            .HasForeignKey(eventoRegistrado => eventoRegistrado.DemandaId)
            .IsRequired();
    }
}
