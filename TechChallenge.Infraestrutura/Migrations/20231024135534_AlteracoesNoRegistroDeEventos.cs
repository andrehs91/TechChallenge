using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallenge.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AlteracoesNoRegistroDeEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demandas_Usuarios_UsuarioResponsavelId",
                schema: "App",
                table: "Demandas");

            migrationBuilder.DropForeignKey(
                name: "FK_EventosRegistrados_Usuarios_UsuarioResponsavelId",
                schema: "App",
                table: "EventosRegistrados");

            migrationBuilder.DropColumn(
                name: "DepartamentoResponsavel",
                schema: "App",
                table: "EventosRegistrados");

            migrationBuilder.RenameColumn(
                name: "UsuarioResponsavelId",
                schema: "App",
                table: "EventosRegistrados",
                newName: "UsuarioSolucionadorId");

            migrationBuilder.RenameIndex(
                name: "IX_EventosRegistrados_UsuarioResponsavelId",
                schema: "App",
                table: "EventosRegistrados",
                newName: "IX_EventosRegistrados_UsuarioSolucionadorId");

            migrationBuilder.RenameColumn(
                name: "UsuarioResponsavelId",
                schema: "App",
                table: "Demandas",
                newName: "UsuarioSolucionadorId");

            migrationBuilder.RenameColumn(
                name: "DepartamentoResponsavel",
                schema: "App",
                table: "Demandas",
                newName: "DepartamentoSolucionador");

            migrationBuilder.RenameIndex(
                name: "IX_Demandas_UsuarioResponsavelId",
                schema: "App",
                table: "Demandas",
                newName: "IX_Demandas_UsuarioSolucionadorId");

            migrationBuilder.RenameColumn(
                name: "DepartamentoResponsavel",
                schema: "App",
                table: "Atividades",
                newName: "DepartamentoSolucionador");

            migrationBuilder.AddForeignKey(
                name: "FK_Demandas_Usuarios_UsuarioSolucionadorId",
                schema: "App",
                table: "Demandas",
                column: "UsuarioSolucionadorId",
                principalSchema: "App",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventosRegistrados_Usuarios_UsuarioSolucionadorId",
                schema: "App",
                table: "EventosRegistrados",
                column: "UsuarioSolucionadorId",
                principalSchema: "App",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demandas_Usuarios_UsuarioSolucionadorId",
                schema: "App",
                table: "Demandas");

            migrationBuilder.DropForeignKey(
                name: "FK_EventosRegistrados_Usuarios_UsuarioSolucionadorId",
                schema: "App",
                table: "EventosRegistrados");

            migrationBuilder.RenameColumn(
                name: "UsuarioSolucionadorId",
                schema: "App",
                table: "EventosRegistrados",
                newName: "UsuarioResponsavelId");

            migrationBuilder.RenameIndex(
                name: "IX_EventosRegistrados_UsuarioSolucionadorId",
                schema: "App",
                table: "EventosRegistrados",
                newName: "IX_EventosRegistrados_UsuarioResponsavelId");

            migrationBuilder.RenameColumn(
                name: "UsuarioSolucionadorId",
                schema: "App",
                table: "Demandas",
                newName: "UsuarioResponsavelId");

            migrationBuilder.RenameColumn(
                name: "DepartamentoSolucionador",
                schema: "App",
                table: "Demandas",
                newName: "DepartamentoResponsavel");

            migrationBuilder.RenameIndex(
                name: "IX_Demandas_UsuarioSolucionadorId",
                schema: "App",
                table: "Demandas",
                newName: "IX_Demandas_UsuarioResponsavelId");

            migrationBuilder.RenameColumn(
                name: "DepartamentoSolucionador",
                schema: "App",
                table: "Atividades",
                newName: "DepartamentoResponsavel");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoResponsavel",
                schema: "App",
                table: "EventosRegistrados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Demandas_Usuarios_UsuarioResponsavelId",
                schema: "App",
                table: "Demandas",
                column: "UsuarioResponsavelId",
                principalSchema: "App",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventosRegistrados_Usuarios_UsuarioResponsavelId",
                schema: "App",
                table: "EventosRegistrados",
                column: "UsuarioResponsavelId",
                principalSchema: "App",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
