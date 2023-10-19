using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Atividade;

public interface IAtividadeRepository
{
    void CriarAtividade(Atividade atividade);
    IList<Atividade> BuscarAtividades();
    IList<Atividade> BuscarAtividadesAtivas();
    IList<Atividade> BuscarAtividadesPorDepartamentoResponsavel(Departamentos departamento);
    Atividade? BuscarAtividade(int id);
    void EditarAtividade(Atividade atividade);
    void ApagarAtividade(Atividade atividade);
}
