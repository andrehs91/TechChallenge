﻿using System.Text.Json.Serialization;
using TechChallenge.Dominio.Enums;

namespace TechChallenge.Dominio.Atividade;

public class Atividade : Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool EstahAtiva { get; set; }
    public string UnidadeResponsavel { get; set; }
    public TiposDeDistribuicao TipoDeDistribuicao { get; set; }
    public Prioridades Prioridade { get; set; }
    public ContagensDePrazo ContagemDePrazo { get; set; }
    public int PrazoEstimado { get; set; }

    public Atividade() { }

    public Atividade(
        string nome,
        string descricao,
        bool estahAtiva,
        string unidadeResponsavel,
        TiposDeDistribuicao tipoDeDistribuicao,
        Prioridades prioridade,
        ContagensDePrazo contagemDePrazo,
        int prazoEstimado)
    {
        Nome = nome;
        Descricao = descricao;
        EstahAtiva = estahAtiva;
        UnidadeResponsavel = unidadeResponsavel;
        TipoDeDistribuicao = tipoDeDistribuicao;
        Prioridade = prioridade;
        ContagemDePrazo = contagemDePrazo;
        PrazoEstimado = prazoEstimado;
    }
}
