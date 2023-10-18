using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Usuario;

public class Usuario : Entidade
{
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public Departamentos Departamento { get; set; }
    public Funcoes Funcao { get; set; }

    public Usuario() { }

    public Usuario(string matricula, string nome, Departamentos departamento)
    {
        Matricula = matricula;
        Nome = nome;
        Departamento = departamento;
    }
}
