namespace TechChallenge.Dominio.Demanda;

public interface IDemandaRepository
{
    void Criar(Demanda demanda);
    IList<Demanda> BuscarTodas();
    Demanda? BuscarPorId(int id);
    void Editar(Demanda demanda);
    void Apagar(Demanda demanda);
}
