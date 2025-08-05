using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaChatMVC.Migrations
{
    /// <inheritdoc />
    public partial class X : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pasword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioChat",
                columns: table => new
                {
                    IdChat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idUser2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioChat", x => x.IdChat);
                    table.ForeignKey(
                        name: "FK_RelacionUsuario1Chat",
                        column: x => x.idUser2,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelacionUsuario2Chat",
                        column: x => x.idUser1,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    Reciber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdChatSended = table.Column<int>(type: "int", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensajesEnviados", x => x.idMensaje);
                    table.ForeignKey(
                        name: "FK_RelacionMensajesChat",
                        column: x => x.IdChatSended,
                        principalTable: "UsuarioChat",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelacionUsuarioEmisor",
                        column: x => x.Sender,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelacionUsuarioReceptor",
                        column: x => x.Reciber,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "id", "Created", "UserName", "isDelete", "pasword" },
                values: new object[,]
                {
                    { new Guid("bacab9dc-ba71-49bf-be42-99dfeda0504c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daniel", false, "123" },
                    { new Guid("cc397046-0bb8-4406-8eb0-d5795ed51512"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Julio", false, "456" }
                });

            migrationBuilder.InsertData(
                table: "UsuarioChat",
                columns: new[] { "IdChat", "idUser1", "idUser2" },
                values: new object[] { 1, new Guid("bacab9dc-ba71-49bf-be42-99dfeda0504c"), new Guid("cc397046-0bb8-4406-8eb0-d5795ed51512") });

            migrationBuilder.CreateIndex(
                name: "IX_MensajesEnviados_IdChatSended",
                table: "MensajesEnviados",
                column: "IdChatSended");

            migrationBuilder.CreateIndex(
                name: "IX_MensajesEnviados_Reciber",
                table: "MensajesEnviados",
                column: "Reciber");

            migrationBuilder.CreateIndex(
                name: "IX_MensajesEnviados_Sender",
                table: "MensajesEnviados",
                column: "Sender");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioChat_idUser1",
                table: "UsuarioChat",
                column: "idUser1");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioChat_idUser2",
                table: "UsuarioChat",
                column: "idUser2");
        }

        /// <inheritdoc />
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
