﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallenge.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDeTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "App");

            migrationBuilder.CreateTable(
                name: "Atividades",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstahAtiva = table.Column<bool>(type: "bit", nullable: false),
                    DepartamentoResponsavel = table.Column<int>(type: "int", nullable: false),
                    TipoDeDistribuicao = table.Column<int>(type: "int", nullable: false),
                    Prioridade = table.Column<int>(type: "int", nullable: false),
                    PrazoEstimado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departamento = table.Column<int>(type: "int", nullable: false),
                    Funcao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Demandas",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtividadeId = table.Column<int>(type: "int", nullable: false),
                    IdDaDemandaReaberta = table.Column<int>(type: "int", nullable: true),
                    MomentoDeAbertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MomentoDeFechamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Prazo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DepartamentoSolicitante = table.Column<int>(type: "int", nullable: false),
                    UsuarioSolicitanteId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoResponsavel = table.Column<int>(type: "int", nullable: false),
                    UsuarioResponsavelId = table.Column<int>(type: "int", nullable: true),
                    Detalhes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Demandas_Atividades_AtividadeId",
                        column: x => x.AtividadeId,
                        principalSchema: "App",
                        principalTable: "Atividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Demandas_Usuarios_UsuarioResponsavelId",
                        column: x => x.UsuarioResponsavelId,
                        principalSchema: "App",
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Demandas_Usuarios_UsuarioSolicitanteId",
                        column: x => x.UsuarioSolicitanteId,
                        principalSchema: "App",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solucionadores",
                schema: "App",
                columns: table => new
                {
                    AtividadesId = table.Column<int>(type: "int", nullable: false),
                    SolucionadoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solucionadores", x => new { x.AtividadesId, x.SolucionadoresId });
                    table.ForeignKey(
                        name: "FK_Solucionadores_Atividades_AtividadesId",
                        column: x => x.AtividadesId,
                        principalSchema: "App",
                        principalTable: "Atividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solucionadores_Usuarios_SolucionadoresId",
                        column: x => x.SolucionadoresId,
                        principalSchema: "App",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventosRegistrados",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemandaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoResponsavel = table.Column<int>(type: "int", nullable: false),
                    UsuarioResponsavelId = table.Column<int>(type: "int", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    MomentoInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MomentoFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosRegistrados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventosRegistrados_Demandas_DemandaId",
                        column: x => x.DemandaId,
                        principalSchema: "App",
                        principalTable: "Demandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventosRegistrados_Usuarios_UsuarioResponsavelId",
                        column: x => x.UsuarioResponsavelId,
                        principalSchema: "App",
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Demandas_AtividadeId",
                schema: "App",
                table: "Demandas",
                column: "AtividadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Demandas_UsuarioResponsavelId",
                schema: "App",
                table: "Demandas",
                column: "UsuarioResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Demandas_UsuarioSolicitanteId",
                schema: "App",
                table: "Demandas",
                column: "UsuarioSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosRegistrados_DemandaId",
                schema: "App",
                table: "EventosRegistrados",
                column: "DemandaId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosRegistrados_UsuarioResponsavelId",
                schema: "App",
                table: "EventosRegistrados",
                column: "UsuarioResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Solucionadores_SolucionadoresId",
                schema: "App",
                table: "Solucionadores",
                column: "SolucionadoresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventosRegistrados",
                schema: "App");

            migrationBuilder.DropTable(
                name: "Solucionadores",
                schema: "App");

            migrationBuilder.DropTable(
                name: "Demandas",
                schema: "App");

            migrationBuilder.DropTable(
                name: "Atividades",
                schema: "App");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "App");
        }
    }
}
