using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Atividade;

public class AtividadeAgragacao
{
    public bool UsuarioPodeCriarAtividade(Usuario.Usuario usuario, Atividade atividade)
    {
        if (usuario.Funcao == Funcoes.Gestor &&
            usuario.CodigoDoDepartamento == atividade.CodigoDoDepartamentoResponsavel)
            return true;
        return false;
    }

    public bool UsuarioPodeEditarAtividade(Usuario.Usuario usuario, Atividade atividade)
    {
        if (usuario.Funcao == Funcoes.Gestor &&
            usuario.CodigoDoDepartamento == atividade.CodigoDoDepartamentoResponsavel)
            return true;
        return false;
    }

    public bool UsuarioPodeApagarAtividade(Usuario.Usuario usuario, Atividade atividade)
    {
        if (usuario.Funcao == Funcoes.Gestor &&
            usuario.CodigoDoDepartamento == atividade.CodigoDoDepartamentoResponsavel)
            return true;
        return false;
    }
}
