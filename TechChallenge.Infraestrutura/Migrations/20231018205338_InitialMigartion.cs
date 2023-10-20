using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallenge.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigartion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstahAtiva = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DepartamentoResponsavel = table.Column<int>(type: "int", nullable: false),
                    TipoDeDistribuicao = table.Column<int>(type: "int", nullable: false),
                    Prioridade = table.Column<int>(type: "int", nullable: false),
                    ContagemDePrazo = table.Column<int>(type: "int", nullable: false),
                    PrazoEstimado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Matricula = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Departamento = table.Column<int>(type: "int", nullable: false),
                    Funcao = table.Column<int>(type: "int", nullable: false),
                    AtividadeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Atividades_AtividadeId",
                        column: x => x.AtividadeId,
                        principalTable: "Atividades",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Demandas",
                columns: table => new
                {
                    NumeroDaDemanda = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AtividadeId = table.Column<int>(type: "int", nullable: true),
                    NumeroDaDemandaReaberta = table.Column<long>(type: "bigint", nullable: false),
                    NumeroDaDemandaDesdobrada = table.Column<long>(type: "bigint", nullable: false),
                    MomentoDeAbertura = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MomentoDeFechamento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Prazo = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DepartamentoSolicitante = table.Column<int>(type: "int", nullable: false),
                    UsuarioSolicitanteId = table.Column<long>(type: "bigint", nullable: false),
                    DepartamentoResponsavel = table.Column<int>(type: "int", nullable: false),
                    UsuarioResponsavelId = table.Column<long>(type: "bigint", nullable: true),
                    Detalhes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demandas", x => x.NumeroDaDemanda);
                    table.ForeignKey(
                        name: "FK_Demandas_Atividades_AtividadeId",
                        column: x => x.AtividadeId,
                        principalTable: "Atividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Demandas_Usuarios_UsuarioResponsavelId",
                        column: x => x.UsuarioResponsavelId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Demandas_Usuarios_UsuarioSolicitanteId",
                        column: x => x.UsuarioSolicitanteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventosRegistrados",
                columns: table => new
                {
                    NumeroDoRegistro = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DemandaNumeroDaDemanda = table.Column<long>(type: "bigint", nullable: true),
                    DepartamentoResponsavel = table.Column<int>(type: "int", nullable: false),
                    UsuarioResponsavelId = table.Column<long>(type: "bigint", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    MomentoInicial = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MomentoFinal = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Mensagem = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosRegistrados", x => x.NumeroDoRegistro);
                    table.ForeignKey(
                        name: "FK_EventosRegistrados_Demandas_DemandaNumeroDaDemanda",
                        column: x => x.DemandaNumeroDaDemanda,
                        principalTable: "Demandas",
                        principalColumn: "NumeroDaDemanda",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventosRegistrados_Usuarios_UsuarioResponsavelId",
                        column: x => x.UsuarioResponsavelId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Demandas_AtividadeId",
                table: "Demandas",
                column: "AtividadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Demandas_UsuarioResponsavelId",
                table: "Demandas",
                column: "UsuarioResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Demandas_UsuarioSolicitanteId",
                table: "Demandas",
                column: "UsuarioSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosRegistrados_DemandaNumeroDaDemanda",
                table: "EventosRegistrados",
                column: "DemandaNumeroDaDemanda");

            migrationBuilder.CreateIndex(
                name: "IX_EventosRegistrados_UsuarioResponsavelId",
                table: "EventosRegistrados",
                column: "UsuarioResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AtividadeId",
                table: "Usuarios",
                column: "AtividadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventosRegistrados");

            migrationBuilder.DropTable(
                name: "Demandas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Atividades");
        }
    }
}
