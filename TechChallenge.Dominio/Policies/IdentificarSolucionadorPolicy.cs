using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Policies;

public static class IdentificarSolucionadorPolicy
{
    public static Usuario.Usuario? IdentificarSolucionador(Atividade.Atividade atividade)
    {
        if (atividade.TipoDeDistribuicao == TiposDeDistribuicao.Manual)
            return null;

        // IMPLEMENTAR
        return null;
    }
}
