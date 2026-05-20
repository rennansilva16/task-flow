using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_TipoDeTarefas_TipoDeTarefaId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioId",
                table: "Tarefas");

            migrationBuilder.DropTable(
                name: "TipoDeTarefas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_TipoDeTarefaId",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Importancia",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "TipoDeTarefaId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Tarefas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Importancia",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TipoDeTarefaId",
                table: "Tarefas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioId",
                table: "Tarefas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoDeTarefas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeTarefas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_TipoDeTarefaId",
                table: "Tarefas",
                column: "TipoDeTarefaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_TipoDeTarefas_TipoDeTarefaId",
                table: "Tarefas",
                column: "TipoDeTarefaId",
                principalTable: "TipoDeTarefas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
