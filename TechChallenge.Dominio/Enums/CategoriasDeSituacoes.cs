using System.ComponentModel;

namespace TechChallenge.Dominio.Enums;

public enum CategoriasDeSituacoes
{
    [Description("Manual")]
    Ativa,

    [Description("Transitória")]
    Transitoria,

    [Description("Inativa")]
    Inativa,
}
