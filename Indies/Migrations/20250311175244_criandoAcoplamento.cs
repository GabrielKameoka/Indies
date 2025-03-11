using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Indies.Migrations
{
    /// <inheritdoc />
    public partial class criandoAcoplamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Musicas",
                nullable: false,
                defaultValue: 9);

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_UsuarioId",
                table: "Musicas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Usuarios_UsuarioId",
                table: "Musicas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Usuarios_UsuarioId",
                table: "Musicas");

            migrationBuilder.DropIndex(
                name: "IX_Musicas_UsuarioId",
                table: "Musicas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Musicas");
        }
    }
}
