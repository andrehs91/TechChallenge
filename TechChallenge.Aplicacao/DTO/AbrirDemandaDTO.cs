using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Aplicacao.DTO;

public class AbrirDemandaDTO
{
    [Required]
    public int id { get; set; }

    [Required]
    public string Detalhes { get; set; }
}
