using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Demanda;

public class DemandaAgragacao
{
    public bool UsuarioPodeEditarDemanda(Usuario.Usuario usuario, Demanda demanda)
    {
        if (usuario.Id == demanda.UsuarioSolicitante.Id)
            return true;
        return false;
    }
}
