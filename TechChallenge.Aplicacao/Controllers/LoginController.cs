using TechChallenge.Aplicacao.DTO;
using TechChallenge.Aplicacao.Services;
using TechChallenge.Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Aplicacao.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ITokenService _tokenService;

    public LoginController(IUsuarioRepositorio usuarioRepositorio, ITokenService tokenService)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Authorize(Roles = "Gestor")]
    [Authorize]
    public IActionResult Test()
    {
        return Ok("Testing...");
    }

    [HttpPost]
    public IActionResult Authenticate([FromBody] LoginDTO loginDTO)
    {
        var usuario = _usuarioRepositorio.BuscarPorCodigoESenha(loginDTO.Matricula, loginDTO.Senha);

        if (usuario == null) return NotFound(new { mensagem = "Matrícula e/ou senha inválidos." });

        var token = _tokenService.GenerateToken(usuario);
        usuario.Senha = null;
        return Ok(new
        {
            Usuario = usuario,
            Token = token
        });
    }
}
