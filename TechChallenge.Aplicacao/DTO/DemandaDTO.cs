using System.Text.Json.Serialization;
using TechChallenge.Aplicacao.Commands;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Demanda;
using TechChallenge.Dominio.Enums;
using TechChallenge.Dominio.Exceptions;
using TechChallenge.Dominio.Usuario;

namespace TechChallenge.Aplicacao.DTO;

public class DemandaDTO
{
    
    public long NumeroDaDemanda { get; set; }
    public int AtividadeId { get; set; }
    public long NumeroDaDemandaReaberta { get; set; }
    public long NumeroDaDemandaDesdobrada { get; set; }
    public DateTime MomentoDeAbertura { get; set; }
    public DateTime MomentoDeFechamento { get; set; }
    public DateTime? Prazo { get; set; }
    //public required Usuario UsuarioSolicitante { get; set; }
    //public Usuario? UsuarioResponsavel { get; set; }
    public string Detalhes { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))] public Situacoes Situacao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Departamentos DepartamentoSolicitante { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Departamentos DepartamentoResponsavel { get; set; }

    public static DemandaDTO EntidadeParaDTO (Demanda demanda)
    {
        return new DemandaDTO()
        {
            NumeroDaDemanda = demanda.NumeroDaDemanda,
            AtividadeId = demanda.atividade.Id,
            NumeroDaDemandaReaberta = demanda.NumeroDaDemandaReaberta,
            NumeroDaDemandaDesdobrada = demanda.NumeroDaDemandaDesdobrada,
            MomentoDeAbertura = demanda.MomentoDeAbertura,
            MomentoDeFechamento = demanda.MomentoDeFechamento,
            //Prazo = demanda.Prazo,
            //UsuarioSolicitante = demanda.UsuarioSolicitante,
            //UsuarioResponsavel = demanda.UsuarioResponsavel,
            Detalhes = demanda.Detalhes,
            Situacao = demanda.Situacao,
            DepartamentoSolicitante = demanda.DepartamentoSolicitante,
            DepartamentoResponsavel = demanda.DepartamentoResponsavel
        };
    }

    public static Demanda DTOParaEntidade(DemandaDTO demandaDTO
                , Atividade atividade
                , Usuario usuarioSolicitante)
    {   
        return new Demanda()
        {
            NumeroDaDemanda = demandaDTO.NumeroDaDemanda,
            atividade = atividade,
            NumeroDaDemandaReaberta = demandaDTO.NumeroDaDemandaReaberta,
            NumeroDaDemandaDesdobrada = demandaDTO.NumeroDaDemandaDesdobrada,
            MomentoDeAbertura = demandaDTO.MomentoDeAbertura,
            MomentoDeFechamento = demandaDTO.MomentoDeFechamento,
            //Prazo = demandaDTO.Prazo,
            UsuarioSolicitante = usuarioSolicitante,
            Detalhes = demandaDTO.Detalhes,
            Situacao = demandaDTO.Situacao,
            DepartamentoSolicitante = demandaDTO.DepartamentoSolicitante,
            DepartamentoResponsavel = demandaDTO.DepartamentoResponsavel
        };
    }
}
