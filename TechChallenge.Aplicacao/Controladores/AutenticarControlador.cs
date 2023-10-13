using TechChallenge.Aplicacao.DTO;
using TechChallenge.Aplicacao.Servicos;
using TechChallenge.Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TechChallenge.Aplicacao.Controladores;

[ApiController]
[Route("/[controller]")]
public class AutenticarControlador : ControllerBase
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ITokenServico _tokenService;

    public AutenticarControlador(IUsuarioRepositorio usuarioRepositorio, ITokenServico tokenService)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Authorize(Roles = "Gestor")]
    public IActionResult Test()
    {
        return Ok("Testing...");
    }

    [HttpPost]
    public IActionResult Authenticate([FromBody] AutenticarDTO AutenticarDTO)
    {
        var usuario = _usuarioRepositorio.BuscarPorCodigoESenha(AutenticarDTO.Matricula, AutenticarDTO.Senha);

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
