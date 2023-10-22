namespace TechChallenge.Aplicacao.DTO;

public class IdsDosUsuariosDTO
{
    public IList<int> IdsDosUsuariosASeremPromovidos { get; set; } = new List<int>();
    public IList<int> IdsDosUsuariosASeremDemovidos { get; set; } = new List<int>();
}
