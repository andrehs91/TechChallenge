using TechChallenge.Aplicacao.DTO;
using TechChallenge.Aplicacao.Services;
using TechChallenge.Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Aplicacao.Controllers;

[ApiController]
[Route("/autenticar")]
public class AutenticarController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepositorio;
    private readonly ITokenService _tokenService;

    public AutenticarController(IUsuarioRepository usuarioRepositorio, ITokenService tokenService)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _tokenService = tokenService;
    }

    [HttpPost]
    public IActionResult Authenticate([FromBody] AutenticarDTO AutenticarDTO)
    {
        string mensagem = "Matrícula e/ou senha inválidos.";
        
        if (AutenticarDTO.Senha != "senha") return NotFound(new { mensagem });
        
        var usuario = _usuarioRepositorio.BuscarPorMatricula(AutenticarDTO.Matricula);
        
        if (usuario == null) return NotFound(new { mensagem });
        
        var token = _tokenService.GenerateToken(usuario);
        return Ok(new
        {
            Usuario = usuario,
            Token = token
        });
    }
}
