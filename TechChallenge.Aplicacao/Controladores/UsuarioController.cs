using TechChallenge.Aplicacao.DTO;
using TechChallenge.Aplicacao.Services;
using TechChallenge.Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
using TechChallenge.Dominio.Enums;
using TechChallenge.Aplicacao.Commands;

namespace TechChallenge.Aplicacao.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioCommand _comandos;
    private readonly ITokenService _tokenService;

    public UsuarioController(UsuarioCommand comandos, ITokenService tokenService)
    {
        _comandos = comandos;
        _tokenService = tokenService;
    }

    [HttpPost("autenticar")]
    public IActionResult Autenticar([FromBody] AutenticarDTO autenticarDTO)
    {
        var usuario = _comandos.Autenticar(autenticarDTO);

        if (usuario is null) return NotFound(new { mensagem = "Matrícula e/ou senha inválidos." });

        return Ok(new
        {
            Usuario = usuario,
            Token = _tokenService.GenerateToken(usuario)
        });
    }

    [HttpGet("listar-usuarios-do-departamento")]
    [Authorize(Roles = "Gestor")]
    public ActionResult<List<Usuario>> ListarUsuariosDoDepartamento()
    {
        var usuario = ObterUsuarioAutenticado();

        return Ok(_comandos.ListarUsuariosDoDepartamento(usuario));
    }

    [HttpPost("definir-gestores-do-departamento")]
    [Authorize(Roles = "Gestor")]
    public ActionResult<RespostaDTO> DefinirGestoresDoDepartamento([FromBody] IdsDosUsuariosDTO idsDosUsuariosDTO)
    {
        RespostaDTO resposta = _comandos.DefinirGestoresDoDepartamento(ObterUsuarioAutenticado(), idsDosUsuariosDTO);
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
