using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Servicos;

public interface ITokenServico
{
    string GenerateToken(Usuario usuario);
}
