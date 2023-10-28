using TechChallenge.Dominio.Entities;

namespace TechChallenge.Aplicacao.DTO;

public class SolucionadorDTO
{
    public int Id { get; set; }
    public string Matricula { get; set; }
    public string Nome { get; set; }

    public SolucionadorDTO(Usuario usuario)
    {
        Id = usuario.Id;
        Matricula = usuario.Matricula;
        Nome = usuario.Nome;
    }
}
