using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.Services;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
}
