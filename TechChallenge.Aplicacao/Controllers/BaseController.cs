using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechChallenge.Dominio.Entities;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Aplicacao.Controllers;

public class BaseController : ControllerBase
{
    protected Usuario ObterUsuarioAutenticado()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var funcao = claimsIdentity!.Claims.Where(c => c.Type == ClaimTypes.Role).Order().First().Value;
        var departamento = claimsIdentity!.FindFirst("Departamento")!.Value;
        return new Usuario()
        {
            Id = int.Parse(claimsIdentity!.FindFirst("Id")!.Value),
            Matricula = claimsIdentity!.FindFirst("Matricula")!.Value,
            Nome = claimsIdentity!.FindFirst("Nome")!.Value,
            Departamento = (Departamentos)Enum.Parse(typeof(Departamentos), departamento),
            Funcao = (Funcoes)Enum.Parse(typeof(Funcoes), funcao)
        };
    }

    protected string ObterErrosDeValidacao()
    {
        return string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
    }
}
