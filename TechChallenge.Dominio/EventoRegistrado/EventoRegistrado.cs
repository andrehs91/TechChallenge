using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.EventoRegistrado;

public class EventoRegistrado
{
    public long Id { get; set; }
    public int DemandaId { get; set; }
    public Demanda.Demanda Demanda { get; set; } = null!;
    public Usuario.Usuario? UsuarioSolucionador { get; set; }
    public Situacoes Situacao { get; set;}
    public DateTime MomentoInicial { get; set;}
    public DateTime? MomentoFinal { get; set; } = null;
    public string? Mensagem { get; set; }

    public EventoRegistrado() { }
}
