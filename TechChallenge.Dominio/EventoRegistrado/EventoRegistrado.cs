using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.EventoRegistrado;

public class EventoRegistrado
{
    public long NumeroDaDemanda { get; set; }
    public short NumeroDoRegistro { get; set;}
    public Departamentos DepartamentoResponsavel { get; set;}
    public Usuario.Usuario UsuarioResponsavel { get; set;}
    public Situacoes Situacao { get; set;}
    public DateTime MomentoInicial { get; set;}
    public DateTime? MomentoFinal { get; set; } = null;
    public string Mensagem { get; set;}
}
