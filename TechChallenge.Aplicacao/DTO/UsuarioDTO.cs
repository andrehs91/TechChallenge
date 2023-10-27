using System.Text.Json.Serialization;
using TechChallenge.Dominio.Entities;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Aplicacao.DTO;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string? Matricula { get; set; }
    public string? Nome { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Departamentos Departamento { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Funcoes Funcao { get; set; }

    public UsuarioDTO(Usuario usuario)
    {
        Id = usuario.Id;
        Matricula = usuario.Matricula;
        Nome = usuario.Nome;
        Departamento = usuario.Departamento;
        Funcao = usuario.Funcao;
    }
}
