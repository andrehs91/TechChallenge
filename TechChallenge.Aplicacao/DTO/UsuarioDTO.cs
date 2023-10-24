using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.DTO;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string? Matricula { get; set; }
    public string? Nome { get; set; }
    public Departamentos Departamento { get; set; }
    public Funcoes Funcao { get; set; }

    public UsuarioDTO(Usuario usuario)
    {
        Id = usuario.Id;
        Matricula = usuario.Matricula;
        Nome = usuario.Nome;
        Departamento = usuario.Departamento;
        Funcao = usuario.Funcao;
    }
}
