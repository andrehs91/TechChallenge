using TechChallenge.Dominio.Entities;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Interfaces;

public interface IDemandaRepository
{
    void Criar(Demanda demanda);
    Demanda? BuscarPorId(int id);
    IList<Demanda> BuscarTodas();
    IList<Demanda> BuscarPorSolicitante(int idSolicitante);
    IList<Demanda> BuscarPorDepartamentoSolicitante(Departamentos departamento);
    IList<Demanda> BuscarPorSolucionador(int idSolucionador);
    IList<Demanda> BuscarPorDepartamentoSolucionador(Departamentos departamento);
    void Editar(Demanda demanda);
    void Apagar(Demanda demanda);
}
