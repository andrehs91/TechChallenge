using TechChallenge.Aplicacao.DTO;
using TechChallenge.Aplicacao.Services;
using TechChallenge.Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TechChallenge.Aplicacao.Commands;
using TechChallenge.Dominio.Exceptions;

namespace TechChallenge.Aplicacao.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : BaseController
{
    private readonly UsuarioCommand _comandos;
    private readonly ITokenService _tokenService;

    public UsuarioController(UsuarioCommand comandos, ITokenService tokenService)
    {
        _comandos = comandos;
        _tokenService = tokenService;
    }

    [HttpPost("autenticar")]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public IActionResult Autenticar([FromBody] AutenticarDTO autenticarDTO)
    {
        // CORRIGIR: Pegar dados de validação
        if (!ModelState.IsValid) throw new ModeloInvalidoException();

        var usuario = _comandos.Autenticar(autenticarDTO);

        if (usuario is null) return NotFound(new RespostaDTO(RespostaDTO.Tipos.Aviso, "Matrícula e/ou senha inválidos."));

        return Ok(new
        {
            Usuario = usuario,
            Token = _tokenService.GenerateToken(usuario)
        });
    }

    [HttpGet("listar-usuarios-do-departamento")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(List<Usuario>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    public ActionResult<List<Usuario>> ListarUsuariosDoDepartamento()
    {
        return Ok(_comandos.ListarUsuariosDoDepartamento(ObterUsuarioAutenticado()));
    }

    [HttpPost("definir-gestores-do-departamento")]
    [Authorize(Roles = "Gestor")]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(RespostaDTO), StatusCodes.Status404NotFound)]
    public ActionResult<RespostaDTO> DefinirGestoresDoDepartamento([FromBody] IdsDosUsuariosDTO idsDosUsuariosDTO)
    {
        RespostaDTO resposta = _comandos.DefinirGestoresDoDepartamento(ObterUsuarioAutenticado(), idsDosUsuariosDTO);
        if (resposta.Tipo == RespostaDTO.Tipos.Erro) return BadRequest(resposta);
        if (resposta.Tipo == RespostaDTO.Tipos.Aviso) return NotFound(resposta);
        return Ok(resposta);
    }
}
