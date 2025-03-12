using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Indies.Migrations
{
    /// <inheritdoc />
    public partial class retidandoReuired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Usuarios_UsuarioId",
                table: "Musicas");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Musicas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Usuarios_UsuarioId",
                table: "Musicas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Usuarios_UsuarioId",
                table: "Musicas");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Musicas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Usuarios_UsuarioId",
                table: "Musicas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
