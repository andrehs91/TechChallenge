using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Usuario;

public class Usuario : Entidade
{
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public string Departamento { get; set; }
    public string? Senha { get; set; }
    public Funcoes Funcao { get; set; }

    public Usuario() { }

    public Usuario(string matricula, string nome, string codigoDoDepartamento)
    {
        Matricula = matricula;
        Nome = nome;
        Departamento = codigoDoDepartamento;
    }
}
