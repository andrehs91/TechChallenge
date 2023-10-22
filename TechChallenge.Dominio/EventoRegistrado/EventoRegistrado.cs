using System.ComponentModel.DataAnnotations;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.EventoRegistrado;

public class EventoRegistrado
{
    [Key]
    public long Id { get; set; }
    public int DemandaId { get; set; }
    public Demanda.Demanda Demanda { get; set; } = null!;
    public Departamentos DepartamentoResponsavel { get; set; }
    public Usuario.Usuario? UsuarioResponsavel { get; set; }
    public Situacoes Situacao { get; set;}
    public DateTime MomentoInicial { get; set;}
    public DateTime? MomentoFinal { get; set; } = null;
    public string Mensagem { get; set; }

    public EventoRegistrado() { }
}
