using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Indies.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoAdministrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Usuarios_UsuarioId",
                table: "Musicas");

            migrationBuilder.DropColumn(
                name: "Chave",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Musicas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuariosModelId",
                table: "Musicas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_UsuariosModelId",
                table: "Musicas",
                column: "UsuariosModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Usuarios_UsuarioId",
                table: "Musicas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Usuarios_UsuariosModelId",
                table: "Musicas",
                column: "UsuariosModelId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Usuarios_UsuarioId",
                table: "Musicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Usuarios_UsuariosModelId",
                table: "Musicas");

            migrationBuilder.DropIndex(
                name: "IX_Musicas_UsuariosModelId",
                table: "Musicas");

            migrationBuilder.DropColumn(
                name: "UsuariosModelId",
                table: "Musicas");

            migrationBuilder.AddColumn<Guid>(
                name: "Chave",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Usuarios",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

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
    }
}
