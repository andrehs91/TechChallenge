using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Usuario;

public class Usuario : Entidade
{
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public string CodigoUnidade { get; set; }
    public string NomeUnidade { get; set; }
    public string? Senha { get; set; }
    public Roles Role { get; set; }

    public Usuario() { }

    public Usuario(string matricula, string nome, string codigoUnidade, string nomeUnidade)
    {
        Matricula = matricula;
        Nome = nome;
        CodigoUnidade = codigoUnidade;
        NomeUnidade = nomeUnidade;
    }
}
