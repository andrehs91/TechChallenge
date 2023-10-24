using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Usuario;

public class Usuario : IEquatable<Usuario>
{
    public int Id { get; set; }
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public Departamentos Departamento { get; set; }
    public Funcoes Funcao { get; set; }
    public virtual List<Atividade.Atividade> Atividades { get; } = new();

    public Usuario() { }

    public Usuario(string matricula, string nome, Departamentos departamento)
    {
        Matricula = matricula;
        Nome = nome;
        Departamento = departamento;
    }

    public bool Equals(Usuario? other)
    {
        if (other is null) return false;
        return Id == other.Id;
    }
}
