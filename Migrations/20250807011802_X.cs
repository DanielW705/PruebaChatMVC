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
                name: "Chats",
                columns: table => new
                {
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfChat = table.Column<int>(type: "int", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.IdChat);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pasword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "MensajesEnviados",
                columns: table => new
                {
                    idMessage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUserSender = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdChatSended = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    visto = table.Column<bool>(type: "bit", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensajesEnviados", x => x.idMessage);
                    table.ForeignKey(
                        name: "FK_RelacionChatMensajes",
                        column: x => x.IdChatSended,
                        principalTable: "Chats",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelacionUsuarioEmisor",
                        column: x => x.IdUserSender,
                        principalTable: "Usuario",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    IdParticipants = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.IdParticipants);
                    table.ForeignKey(
                        name: "ChatParticipants",
                        column: x => x.IdChat,
                        principalTable: "Chats",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelacionParticipantesChat",
                        column: x => x.IdUser,
                        principalTable: "Usuario",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "IdChat", "ChatDescription", "ChatName", "TypeOfChat" },
                values: new object[] { new Guid("d7917cfe-0e42-4f57-b237-8a44ae5d3d20"), null, "Chat de Daniel y Julio", 0 });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "IdUser", "UserName", "pasword" },
                values: new object[,]
                {
                    { new Guid("bacab9dc-ba71-49bf-be42-99dfeda0504c"), "Daniel", "123" },
                    { new Guid("cc397046-0bb8-4406-8eb0-d5795ed51512"), "Julio", "456" }
                });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "IdParticipants", "IdChat", "IdUser" },
                values: new object[,]
                {
                    { 1, new Guid("d7917cfe-0e42-4f57-b237-8a44ae5d3d20"), new Guid("bacab9dc-ba71-49bf-be42-99dfeda0504c") },
                    { 2, new Guid("d7917cfe-0e42-4f57-b237-8a44ae5d3d20"), new Guid("cc397046-0bb8-4406-8eb0-d5795ed51512") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MensajesEnviados_IdChatSended",
                table: "MensajesEnviados",
                column: "IdChatSended");

            migrationBuilder.CreateIndex(
                name: "IX_MensajesEnviados_IdUserSender",
                table: "MensajesEnviados",
                column: "IdUserSender");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_IdChat",
                table: "Participants",
                column: "IdChat");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_IdUser",
                table: "Participants",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MensajesEnviados");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
