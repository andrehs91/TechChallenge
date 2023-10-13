﻿using System.Text.Json.Serialization;
using TechChallenge.Dominio.Atividade;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Aplicacao.DTO;

public class AtividadeDTO
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    public string UnidadeResponsavel { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public Prioridades Prioridade { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public ContagensDePrazo ContagemDePrazo { get; set; }
    public int PrazoEstimado { get; set; }

    public static AtividadeDTO EntidadeParaDTO (Atividade atividade)
    {
        return new AtividadeDTO() {
            Nome = atividade.Nome,
            Descricao = atividade.Descricao,
            EstahAtiva = atividade.EstahAtiva,
            UnidadeResponsavel = atividade.UnidadeResponsavel,
            TipoDeDistribuicao = atividade.TipoDeDistribuicao,
            Prioridade = atividade.Prioridade,
            ContagemDePrazo = atividade.ContagemDePrazo,
            PrazoEstimado = atividade.PrazoEstimado
        };
    }

    public static Atividade DTOParaEntidade(AtividadeDTO atividadeDTO)
    {
        return new Atividade()
        {
            Nome = atividadeDTO.Nome,
            Descricao = atividadeDTO.Descricao,
            EstahAtiva = atividadeDTO.EstahAtiva,
            UnidadeResponsavel = atividadeDTO.UnidadeResponsavel,
            TipoDeDistribuicao = atividadeDTO.TipoDeDistribuicao,
            Prioridade = atividadeDTO.Prioridade,
            ContagemDePrazo = atividadeDTO.ContagemDePrazo,
            PrazoEstimado = atividadeDTO.PrazoEstimado
        };
    }
}
