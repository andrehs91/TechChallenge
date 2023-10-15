using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechChallenge.Aplicacao.Comandos;
using TechChallenge.Aplicacao.DTO;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Excecoes;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Controladores;

[ApiController]
[Route("/atividade")]
public class AtividadeControlador : ControllerBase
{
    private readonly AtividadeComandos _comandos;

    public AtividadeControlador(AtividadeComandos comandos)
    {
        _comandos = comandos;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IList<AtividadeDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IList<AtividadeDTO>> BuscarAtividades()
    {
        var listaDeAtividadesDTO = _comandos.BuscarAtividades()
            .Select(AtividadeDTO.EntidadeParaDTO)
            .ToList();
        return Ok(listaDeAtividadesDTO);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AtividadeDTO> BuscarAtividade(int id)
    {
        return _comandos.BuscarAtividade(id) is Atividade atividade
            ? Ok(AtividadeDTO.EntidadeParaDTO(atividade))
            : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(AtividadeDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult<AtividadeDTO> CriarAtividade(CriarAtividadeDTO criarAtividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        var usuario = ObterUsuario();
        var atividadeDTO = criarAtividadeDTO.ConverterParaAtividadeDTO(usuario.Departamento);

        try
        {
            var resposta = _comandos.CriarAtividade(usuario, atividadeDTO);
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
    public IActionResult EditarAtividade(int id, EditarAtividadeDTO editarAtividadeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(new BadRequestObjectResult(ModelState));

        var usuario = ObterUsuario();
        var atividadeDTO = editarAtividadeDTO.ConverterParaAtividadeDTO(id, usuario.Departamento);

        try
        {
            return _comandos.EditarAtividade(usuario, atividadeDTO)
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
            return _comandos.ApagarAtividade(ObterUsuario(), id)
                ? NoContent()
                : NotFound();
        }
        catch (UsuarioNaoAutorizadoExcecao)
        {
            return Forbid();
        }
    }

    private Usuario ObterUsuario()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var funcao = claimsIdentity!.Claims.Where(c => c.Type == ClaimTypes.Role).Order().First().Value;
        return new Usuario()
        {
            Matricula = claimsIdentity!.FindFirst("Matricula")!.Value,
            Nome = claimsIdentity!.FindFirst("Nome")!.Value,
            Departamento = claimsIdentity!.FindFirst("Departamento")!.Value,
            Funcao = (Funcoes)Enum.Parse(typeof(Funcoes), funcao)
        };
    }
}
