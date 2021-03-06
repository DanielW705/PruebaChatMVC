using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PruebaChatMVC.Migrations
{
    public partial class Holamundo : Migration
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
                name: "MensajesEnviados",
                columns: table => new
                {
                    idMensaje = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameSender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaDeEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    visto = table.Column<bool>(type: "bit", nullable: false),
                    Sender = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reciber = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensajesEnviados", x => x.idMensaje);
                    table.ForeignKey(
                        name: "Relacion_Usuario_Emisor",
                        column: x => x.Sender,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Relacion_Usuario_Receptor",
                        column: x => x.Reciber,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                values: new object[] { new Guid("aa794625-afcd-4b73-b109-6a9cbda775d7"), "123", "Daniel" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "id", "Pasword", "UserName" },
                values: new object[] { new Guid("81ea2a69-193a-4e91-b11d-c3ca68e77792"), "456", "Julio" });

            migrationBuilder.CreateIndex(
                name: "IX_MensajesEnviados_Reciber",
                table: "MensajesEnviados",
                column: "Reciber");

            migrationBuilder.CreateIndex(
                name: "IX_MensajesEnviados_Sender",
                table: "MensajesEnviados",
                column: "Sender");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MensajesEnviados");

            migrationBuilder.DropTable(
                name: "UsuarioChat");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
