using TechChallenge.Dominio.Entities;

namespace TechChallenge.Aplicacao.DTO;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string? Matricula { get; set; }
    public string? Nome { get; set; }
    public string Departamento { get; set; }
    public bool EhGestor { get; set; } = false;

    public UsuarioDTO(Usuario usuario)
    {
        Id = usuario.Id;
        Matricula = usuario.Matricula;
        Nome = usuario.Nome;
        Departamento = usuario.Departamento;
        EhGestor = usuario.EhGestor;
    }
}
