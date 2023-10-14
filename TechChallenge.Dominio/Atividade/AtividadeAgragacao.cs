using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Dominio.Atividade;

public class AtividadeAgragacao
{
    public bool UsuarioPodeCriarAtividade(PapelEDepartamentoDoUsuario dadosDoUsuario, Atividade atividade)
    {
        if (dadosDoUsuario.Papel == "Gestor" & dadosDoUsuario.CodigoDoDepartamento == atividade.CodigoDoDepartamentoResponsavel)
            return true;
        return false;
    }

    public bool UsuarioPodeEditarAtividade(PapelEDepartamentoDoUsuario dadosDoUsuario, Atividade atividade)
    {
        if (dadosDoUsuario.Papel == "Gestor" & dadosDoUsuario.CodigoDoDepartamento == atividade.CodigoDoDepartamentoResponsavel)
            return true;
        return false;
    }

    public bool UsuarioPodeApagarAtividade(PapelEDepartamentoDoUsuario dadosDoUsuario, Atividade atividade)
    {
        if (dadosDoUsuario.Papel == "Gestor" & dadosDoUsuario.CodigoDoDepartamento == atividade.CodigoDoDepartamentoResponsavel)
            return true;
        return false;
    }
}
