namespace TechChallenge.Dominio.Atividade;

public interface IAtividadeRepositorio
{
    void CriarAtividade(Atividade atividade);
    IList<Atividade> BuscarAtividades();
    Atividade? BuscarAtividade(int id);
    void EditarAtividade(Atividade atividade);
    void ApagarAtividade(Atividade atividade);
}
