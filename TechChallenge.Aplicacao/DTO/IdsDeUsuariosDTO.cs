namespace TechChallenge.Aplicacao.DTO;

public class IdsDosUsuariosDTO
{
    public IList<long> IdsDosUsuariosASeremPromovidos { get; set; } = new List<long>();
    public IList<long> IdsDosUsuariosASeremDemovidos { get; set; } = new List<long>();
}
