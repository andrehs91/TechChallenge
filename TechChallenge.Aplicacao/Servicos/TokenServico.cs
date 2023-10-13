﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Servicos;

public class TokenServico : ITokenServico
{
    private readonly IConfiguration _configuration;

    public TokenServico(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Usuario usuario)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, usuario.Matricula),
            new Claim(ClaimTypes.Role, usuario.Role.ToString()),
            new Claim("Matricula", usuario.Matricula),
            new Claim("Nome", usuario.Nome),
            new Claim("CodigoUnidade", usuario.CodigoUnidade),
            new Claim("NomeUnidade", usuario.NomeUnidade)
        };
        //foreach (var Role in usuario.Roles)
        //{
        //    claims.Add(new Claim(ClaimTypes.Role, Role.ToString()));
        //}

        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = _configuration.GetValue<string>("Secret");
        var key = Encoding.ASCII.GetBytes(secret!);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
