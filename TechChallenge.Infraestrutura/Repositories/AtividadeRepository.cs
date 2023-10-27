using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TechChallenge.Dominio.Entities;
using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Interfaces;
using TechChallenge.Infraestrutura.Data;
using TechChallenge.Dominio.Policies;

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
        return _context.Atividades.Find(id);
    }

    public Atividade? BuscarPorIdComSolucionadores(int id)
    {
        return _context.Atividades
            .Include(a => a.Solucionadores)
            .FirstOrDefault(a => a.Id == id);
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

    public Usuario? IdentificarSolucionadorMenosAtarefado(int id)
    {
        var atividade = BuscarPorIdComSolucionadores(id);
        if (atividade is null) return null;

        var solucionadores = atividade.Solucionadores;
        if (!solucionadores.Any()) return null;

        var demandas = _context.Demandas
            .Where(d => solucionadores.Select(s => s.Id).Contains(d.UsuarioSolucionadorId ?? -1))
            .Select(d => new { d.UsuarioSolucionadorId, PrazoEstimado = (int)d.Prazo.Subtract(d.MomentoDeAbertura).TotalMinutes })
            .ToList();

        List<RankDTO> rank = new();
        foreach (var solucionador in solucionadores)
        {
            rank.Add(new RankDTO()
            {
                Solucionador = solucionador,
                PrazoEstimadoTotal = demandas.Where(d => d.UsuarioSolucionadorId == solucionador.Id).Sum(d => d.PrazoEstimado),
                QuantidadeDeDemandas = demandas.Where(d => d.UsuarioSolucionadorId == solucionador.Id).Count(),
            });
        }

        return rank
            .OrderBy(r => r.PrazoEstimadoTotal)
            .OrderBy(r => r.QuantidadeDeDemandas)
            .First()
            .Solucionador;
    }
}
