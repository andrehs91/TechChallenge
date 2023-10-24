using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Atividade;

public interface IAtividadeRepository
{
    void Criar(Atividade atividade);
    Atividade? BuscarPorId(int id);
    IList<Atividade> BuscarTodas();
    IList<Atividade> BuscarAtivas();
    IList<Atividade> BuscarPorDepartamentoSolucionador(Departamentos departamento);
    void Editar(Atividade atividade);
    void Apagar(Atividade atividade);
}
