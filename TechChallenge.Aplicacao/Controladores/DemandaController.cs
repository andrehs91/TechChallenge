using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechChallenge.Aplicacao.Commands;
using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Demanda;
using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Exceptions;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Controllers;

[ApiController]
[Route("/demanda")]
public class DemandaController : ControllerBase
{
    private readonly DemandaCommand _comandos;
    private readonly AtividadeCommand _comandosAtividade;


    public DemandaController(DemandaCommand comandos, AtividadeCommand comandosAtividade)
    {
        _comandos = comandos;
        _comandosAtividade = comandosAtividade;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IList<DemandaDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IList<DemandaDTO>> BuscaDemandas()
    {
        var listaDeDemandasDTO = _comandos.BuscarDemandas()
            .Select(DemandaDTO.EntidadeParaDTO)
            .ToList();
        return Ok(listaDeDemandasDTO);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DemandaDTO> BuscarDemanda(int id)
    {
        return _comandos.BuscarDemanda(id) is Demanda demanda
            ? Ok(DemandaDTO.EntidadeParaDTO(demanda))
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(DemandaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult<DemandaDTO> CriarDemanda(CriarDemandaDTO criarDemandaDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        var usuario = ObterUsuario();
        var demandaDTO = criarDemandaDTO.ConverterParaDemandaDTO();

        try
        {
            var resposta = _comandos.CriarDemanda(usuario, demandaDTO);
            return CreatedAtAction(nameof(BuscarDemanda), new { id = resposta.Item1 }, resposta.Item2);
        }
        catch (UsuarioNaoAutorizadoException)
        {
            return Forbid();
        }
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult FecharDemanda(int id)
    {
        try
        {
            return _comandos.FecharDemanda(ObterUsuario(), id)
                ? NoContent()
                : NotFound();
        }
        catch (UsuarioNaoAutorizadoException)
        {
            return Forbid();
        }
    }

    private Usuario ObterUsuario()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var funcao = claimsIdentity!.Claims.Where(c => c.Type == ClaimTypes.Role).Order().First().Value;
        var departamento = claimsIdentity!.FindFirst("Departamento")!.Value;
        return new Usuario()
        {
            Matricula = claimsIdentity!.FindFirst("Matricula")!.Value,
            Nome = claimsIdentity!.FindFirst("Nome")!.Value,
            Departamento = (Departamentos)Enum.Parse(typeof(Departamentos), departamento),
            Funcao = (Funcoes)Enum.Parse(typeof(Funcoes), funcao)
        };
    }
}
