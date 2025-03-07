using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Indies.Migrations
{
    /// <inheritdoc />
    public partial class MudandoNome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Musicas",
                table: "Musicas",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Musicas",
                newName: "Musicas");
        }
    }
}
