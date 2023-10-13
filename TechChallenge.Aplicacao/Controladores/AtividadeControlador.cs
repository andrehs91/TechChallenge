using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Infraestrutura.Repositorios;

namespace TechChallenge.Aplicacao.Controladores;

[ApiController]
[Route("/[controller]")]
public class AtividadeControlador : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly string NullEntityDetail = "Entity set 'ApplicationDbContext.Atividade' is null.";

    public AtividadeControlador(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AtividadeDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AtividadeDTO>>> BuscarAtividades()
    {
        if (_context.Atividades == null) return Problem(NullEntityDetail);

        return await _context.Atividades.Select(atividade => AtividadeDTO.EntidadeParaDTO(atividade)).ToListAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<AtividadeDTO>> BuscarAtividade(long id)
    {
        if (_context.Atividades == null) return Problem(NullEntityDetail);

        return await _context.Atividades.FindAsync(id) is Atividade atividade
            ? Ok(AtividadeDTO.EntidadeParaDTO(atividade))
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<AtividadeDTO>> CriarAtividade(AtividadeDTO atividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        if (_context.Atividades == null) return Problem(NullEntityDetail);

        Atividade atividade = AtividadeDTO.DTOParaEntidade(atividadeDTO);

        _context.Atividades.Add(atividade);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarAtividade), new { id = atividade.Id }, AtividadeDTO.EntidadeParaDTO(atividade));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditarAtividade(long id, AtividadeDTO atividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        if (_context.Atividades == null) return Problem(NullEntityDetail);

        var atividade = await _context.Atividades.FindAsync(id);

        if (atividade is null) return NotFound();

        atividade.Nome = atividadeDTO.Nome;
        atividade.Descricao = atividadeDTO.Descricao;
        atividade.EstahAtiva = atividadeDTO.EstahAtiva;
        atividade.UnidadeResponsavel = atividadeDTO.UnidadeResponsavel;
        atividade.TipoDeDistribuicao = atividadeDTO.TipoDeDistribuicao;
        atividade.Prioridade = atividadeDTO.Prioridade;
        atividade.ContagemDePrazo = atividadeDTO.ContagemDePrazo;
        atividade.PrazoEstimado = atividadeDTO.PrazoEstimado;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ApagarAtividade(long id)
    {
        if (_context.Atividades == null) return Problem(NullEntityDetail);

        var atividade = await _context.Atividades.FindAsync(id);

        if (atividade is null) return NotFound();

        _context.Atividades.Remove(atividade);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
