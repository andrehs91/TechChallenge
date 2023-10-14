using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechChallenge.Aplicacao.Comandos;
using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Excecoes;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Controladores;

[ApiController]
[Route("/Atividade")]
public class AtividadeControlador : ControllerBase
{
    private readonly AtividadeComandos _comandos;

    public AtividadeControlador(AtividadeComandos comandos)
    {
        _comandos = comandos;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<AtividadeDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IList<AtividadeDTO>> BuscarAtividades()
    {
        return Ok(_comandos.BuscarAtividades());
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AtividadeDTO> BuscarAtividade(int id)
    {
        return _comandos.BuscarAtividade(id) is AtividadeDTO atividadeDTO
            ? Ok(atividadeDTO)
            : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult<AtividadeDTO> CriarAtividade(AtividadeDTO atividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        try
        {
            var resposta = _comandos.CriarAtividade(ObterDadosDoUsuario(), atividadeDTO);
            return CreatedAtAction(nameof(BuscarAtividade), new { id = resposta.Item1 }, resposta.Item2);
        }
        catch (UsuarioNaoAutorizadoExcecao)
        {
            return Forbid();
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult EditarAtividade(int id, AtividadeDTO atividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        try
        {
            return _comandos.EditarAtividade(ObterDadosDoUsuario(), id, atividadeDTO)
                ? NoContent()
                : NotFound();
        }
        catch (UsuarioNaoAutorizadoExcecao)
        {
            return Forbid();
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ApagarAtividade(int id)
    {
        try
        {
            return _comandos.ApagarAtividade(ObterDadosDoUsuario(), id)
                ? NoContent()
                : NotFound();
        }
        catch (UsuarioNaoAutorizadoExcecao)
        {
            return Forbid();
        }
    }

    private PapelEDepartamentoDoUsuario ObterDadosDoUsuario()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        return new PapelEDepartamentoDoUsuario()
        {
            Papel = claimsIdentity!.Claims.Where(c => c.Type == ClaimTypes.Role).Order().First().Value,
            CodigoDoDepartamento = claimsIdentity!.FindFirst("CodigoDoDepartamento")!.Value
        };
    }
}
