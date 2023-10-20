using System.ComponentModel;

namespace TechChallenge.Aplicacao.DTO;

public class AutenticarDTO
{
    public string Matricula { get; set; }

    [DefaultValue("senha")]
    public string Senha { get; set; }
}
