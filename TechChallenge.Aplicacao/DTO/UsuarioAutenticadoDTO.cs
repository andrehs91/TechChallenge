namespace TechChallenge.Aplicacao.DTO;

public class UsuarioAutenticadoDTO
{
    public UsuarioDTO Usuario { get; set; }
    public string Token { get; set; }

    public UsuarioAutenticadoDTO(UsuarioDTO usuarioDTO, string token)
    {
        Usuario = usuarioDTO;
        Token = token;
    }
}
