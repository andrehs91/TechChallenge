using Microsoft.AspNetCore.Mvc;
using TechChallenge.Aplicacao.Comandos;
using TechChallenge.Aplicacao.DTO;

namespace TechChallenge.Aplicacao.Controladores;

[ApiController]
[Route("/[controller]")]
public class AtividadeControlador : ControllerBase
{
    private readonly AtividadeComandos _comandos;

    public AtividadeControlador(AtividadeComandos comandos)
    {
        _comandos = comandos;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AtividadeDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AtividadeDTO>>> BuscarAtividades()
    {
        return Ok(await _comandos.BuscarAtividades());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status404NotFound)] // CORRIGIR TIPO RETORNADO
    public async Task<ActionResult<AtividadeDTO>> BuscarAtividade(long id)
    {
        return await _comandos.BuscarAtividade(id) is AtividadeDTO atividadeDTO
            ? Ok(atividadeDTO)
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status400BadRequest)] // CORRIGIR TIPO RETORNADO
    public async Task<ActionResult<AtividadeDTO>> CriarAtividade(AtividadeDTO atividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        var resposta = await _comandos.CriarAtividade(atividadeDTO);
        return CreatedAtAction(nameof(BuscarAtividade), new { id = resposta.Item1 }, resposta.Item2);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status400BadRequest)] // CORRIGIR TIPO RETORNADO
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status404NotFound)] // CORRIGIR TIPO RETORNADO
    public async Task<IActionResult> EditarAtividade(long id, AtividadeDTO atividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        return await _comandos.EditarAtividade(id, atividadeDTO)
            ? NoContent()
            : NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status404NotFound)] // CORRIGIR TIPO RETORNADO
    public async Task<IActionResult> ApagarAtividade(long id)
    {
        return await _comandos.ApagarAtividade(id)
            ? NoContent()
            : NotFound();
    }
}
