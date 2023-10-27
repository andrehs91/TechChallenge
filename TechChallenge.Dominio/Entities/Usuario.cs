﻿namespace TechChallenge.Dominio.Entities;

public class Usuario : IEquatable<Usuario>
{
    public int Id { get; set; }
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public string Departamento { get; set; }
    public bool EhGestor { get; set; } = false;
    public virtual List<Atividade> Atividades { get; } = new();

    public Usuario() { }

    public Usuario(string matricula, string nome, string departamento)
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
