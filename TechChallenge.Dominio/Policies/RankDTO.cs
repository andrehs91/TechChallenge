using TechChallenge.Dominio.Entities;

namespace TechChallenge.Dominio.Policies;

public class RankDTO
{
    public Usuario Solucionador { get; set; }
    public int PrazoEstimadoTotal { get; set; }
    public int QuantidadeDeDemandas  { get; set; }
}
