using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechChallenge.Aplicacao.Commands;
using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Exceptions;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Controllers;

[ApiController]
[Route("[controller]")]
public class AtividadeController : ControllerBase
{
    private readonly AtividadeCommand _comandos;

    public AtividadeController(AtividadeCommand comandos)
    {
        _comandos = comandos;
    }

    [HttpGet("ativas")]
    [Authorize]
    [ProducesResponseType(typeof(IList<AtividadeDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IList<AtividadeDTO>> ListarAtividadesAtivas()
    {
        var listaDeAtividadesDTO = _comandos.ListarAtividadesAtivas()
            .Select(AtividadeDTO.EntidadeParaDTO)
            .ToList();
        return Ok(listaDeAtividadesDTO);
    }

    [HttpGet("do-departamento")]
    [Authorize]
    [ProducesResponseType(typeof(IList<AtividadeDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IList<AtividadeDTO>> ListarAtividadesDoDepartamento()
    {
        var usuario = ObterUsuarioAutenticado();
        var listaDeAtividadesDTO = _comandos.ListarAtividadesPorDepartamentoResponsavel(usuario)
            .Select(AtividadeDTO.EntidadeParaDTO)
            .ToList();
        return Ok(listaDeAtividadesDTO);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AtividadeDTO> ConsultarAtividade(int id)
    {
        return _comandos.ConsultarAtividade(id) is Atividade atividade
            ? Ok(AtividadeDTO.EntidadeParaDTO(atividade))
            : NotFound(new RespostaDTO(RespostaDTO.Tipos.Aviso, "Atividade não encontrada."));
    }

    [HttpPost("criar/")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult<AtividadeDTO> CriarAtividade(CriarAtividadeDTO criarAtividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        var usuario = ObterUsuarioAutenticado();
        var atividadeDTO = criarAtividadeDTO.ConverterParaAtividadeDTO(usuario.Departamento);

        try
        {
            Atividade atividade = _comandos.CriarAtividade(usuario, atividadeDTO);
            return CreatedAtAction(nameof(ConsultarAtividade), new { id = atividade.Id }, AtividadeDTO.EntidadeParaDTO(atividade));
        }
        catch (UsuarioNaoAutorizadoException)
        {
            return Forbid();
        }
    }

    [HttpPut("editar/{id}")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult EditarAtividade(int id, EditarAtividadeDTO editarAtividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        var usuario = ObterUsuarioAutenticado();
        var atividadeDTO = editarAtividadeDTO.ConverterParaAtividadeDTO(id, usuario.Departamento);

        try
        {
            return _comandos.EditarAtividade(usuario, atividadeDTO)
                ? NoContent()
                : NotFound();
        }
        catch (UsuarioNaoAutorizadoException)
        {
            return Forbid();
        }
    }

    [HttpDelete("apagar/{id}")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ApagarAtividade(int id)
    {
        try
        {
            return _comandos.ApagarAtividade(ObterUsuarioAutenticado(), id)
                ? NoContent()
                : NotFound();
        }
        catch (UsuarioNaoAutorizadoException)
        {
            return Forbid();
        }
    }

    [HttpPost("{id}/definir-solucionadores")]
    [Authorize(Roles = "Gestor")]
    public ActionResult<RespostaDTO> DefinirSolucionadores([FromBody] IdsDosUsuariosDTO idsDosUsuariosDTO, int id)
    {
        RespostaDTO resposta = _comandos.DefinirSolucionadores(ObterUsuarioAutenticado(), idsDosUsuariosDTO, id);
        if (resposta.Tipo == RespostaDTO.Tipos.Erro) return BadRequest(resposta);
        if (resposta.Tipo == RespostaDTO.Tipos.Aviso) return NotFound(resposta);
        return Ok(resposta);
    }

    private Usuario ObterUsuarioAutenticado()
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
