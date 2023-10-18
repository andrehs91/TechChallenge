namespace TechChallenge.Dominio.EventoRegistrado;

public interface IEventoRegistradoRepository
{
    void CriarEvento(EventoRegistrado evento);
    IList<EventoRegistrado> BuscarEventos();
    EventoRegistrado? BuscarEvento(long numeroDoRegistro);
    void EditarEvento(EventoRegistrado evento);
    void ApagarEvento(EventoRegistrado evento);
}
