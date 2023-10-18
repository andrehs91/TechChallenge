using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.EventoRegistrado;

public class EventoRegistrado
{
    public Demanda.Demanda Demanda { get; set; }
    public short NumeroDoRegistro { get; set;}
    public Departamentos DepartamentoResponsavel { get; set;}
    public Usuario.Usuario UsuarioResponsavel { get; set;}
    public Situacoes Situacao { get; set;}
    public DateTime MomentoInicial { get; set;}
    public DateTime MomentoFinal { get; set;}
    public string Mensagem { get; set;}
}
