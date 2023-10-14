using Microsoft.EntityFrameworkCore;
using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Infraestrutura.Repositorios;

namespace TechChallenge.Aplicacao.Comandos;

public class AtividadeComandos
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AtividadeComandos> _logger;

    public AtividadeComandos(ApplicationDbContext context, ILogger<AtividadeComandos> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Tuple<long, AtividadeDTO>> CriarAtividade(AtividadeDTO atividadeDTO)
    {
        Atividade atividade = AtividadeDTO.DTOParaEntidade(atividadeDTO);
        _context.Atividades.Add(atividade);
        await _context.SaveChangesAsync();
        return Tuple.Create(atividade.Id, AtividadeDTO.EntidadeParaDTO(atividade));
    }

    public async Task<IEnumerable<AtividadeDTO>> BuscarAtividades()
    {
        return await _context.Atividades.Select(atividade => AtividadeDTO.EntidadeParaDTO(atividade)).ToListAsync();
    }

    public async Task<AtividadeDTO?> BuscarAtividade(long id)
    {
        return await _context.Atividades.FindAsync(id) is Atividade atividade
            ? AtividadeDTO.EntidadeParaDTO(atividade)
            : null;
    }

    public async Task<bool> EditarAtividade(long id, AtividadeDTO atividadeDTO)
    {
        var atividade = await _context.Atividades.FindAsync(id);

        if (atividade is null) return false;

        atividade.Nome = atividadeDTO.Nome;
        atividade.Descricao = atividadeDTO.Descricao;
        atividade.EstahAtiva = atividadeDTO.EstahAtiva;
        atividade.UnidadeResponsavel = atividadeDTO.UnidadeResponsavel;
        atividade.TipoDeDistribuicao = atividadeDTO.TipoDeDistribuicao;
        atividade.Prioridade = atividadeDTO.Prioridade;
        atividade.ContagemDePrazo = atividadeDTO.ContagemDePrazo;
        atividade.PrazoEstimado = atividadeDTO.PrazoEstimado;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ApagarAtividade(long id)
    {
        var atividade = await _context.Atividades.FindAsync(id);

        if (atividade is null) return false;

        _context.Atividades.Remove(atividade);
        await _context.SaveChangesAsync();

        return true;
    }
}
