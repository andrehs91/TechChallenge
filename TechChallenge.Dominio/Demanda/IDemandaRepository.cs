namespace TechChallenge.Dominio.Demanda;

public interface IDemandaRepository
{
    void CriarDemanda(Demanda demanda);
    IList<Demanda> BuscarDemandas();
    Demanda? BuscarDemanda(long numeroDaDemanda);
    void EditarDemanda(Demanda demanda);
    void ApagarDemanda(Demanda demanda);
}
