using TechChallenge.Aplicacao.DTO;
using TechChallenge.Aplicacao.Servicos;
using TechChallenge.Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Aplicacao.Controladores;

[ApiController]
[Route("/autenticar")]
public class AutenticarControlador : ControllerBase
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ITokenServico _tokenService;

    public AutenticarControlador(IUsuarioRepositorio usuarioRepositorio, ITokenServico tokenService)
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
