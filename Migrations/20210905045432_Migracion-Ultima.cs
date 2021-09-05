using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PruebaChatMVC.Migrations
{
    public partial class MigracionUltima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Pasword = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioChat",
                columns: table => new
                {
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idChat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioChat", x => x.idUser);
                    table.ForeignKey(
                        name: "Relacion_Usuario_Chat",
                        column: x => x.idUser,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "id", "Pasword", "UserName" },
                values: new object[] { new Guid("cc63eea7-2d0f-4508-93d5-7673d8a2bccc"), "123", "Daniel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioChat");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
