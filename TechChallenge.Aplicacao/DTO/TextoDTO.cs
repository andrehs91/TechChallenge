using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Aplicacao.DTO;

public class TextoDTO
{
    [Required]
    [MinLength(3)]
    public string Conteudo { get; set; }
}
