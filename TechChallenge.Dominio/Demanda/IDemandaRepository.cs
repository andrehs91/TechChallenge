using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Demanda;

public interface IDemandaRepository
{
    void Criar(Demanda demanda);
    Demanda? BuscarPorId(int id);
    IList<Demanda> BuscarTodas();
    IList<Demanda> BuscarPorSolicitante(int idSolicitante);
    IList<Demanda> BuscarPorDepartamentoSolicitante(Departamentos departamento);
    IList<Demanda> BuscarPorResponsavel(int idResponsavel);
    IList<Demanda> BuscarPorDepartamentoResponsavel(Departamentos departamento);
    void Editar(Demanda demanda);
    void Apagar(Demanda demanda);
}
