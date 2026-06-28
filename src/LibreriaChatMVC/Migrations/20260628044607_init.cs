using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibreriaChatMVC.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatBase",
                columns: table => new
                {
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBase", x => x.IdChat);
                });

            migrationBuilder.CreateTable(
                name: "EstadosDeConexion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sumary = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosDeConexion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sumary = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChatsGrupales",
                columns: table => new
                {
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatsGrupales", x => x.IdChat);
                    table.ForeignKey(
                        name: "FK_ChatsGrupales_ChatBase_IdChat",
                        column: x => x.IdChat,
                        principalTable: "ChatBase",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_Rol",
                        column: x => x.Rol,
                        principalTable: "Roles",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ChatsIndividuales",
                columns: table => new
                {
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Emisor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Receptor = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatsIndividuales", x => x.IdChat);
                    table.ForeignKey(
                        name: "FK_ChatsIndividuales_ChatBase_IdChat",
                        column: x => x.IdChat,
                        principalTable: "ChatBase",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatsIndividuales_Usuarios_Emisor",
                        column: x => x.Emisor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatsIndividuales_Usuarios_Receptor",
                        column: x => x.Receptor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Estatuses",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdEstatus = table.Column<int>(type: "int", nullable: false),
                    UltimaConextion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatuses", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Estatuses_EstadosDeConexion_IdEstatus",
                        column: x => x.IdEstatus,
                        principalTable: "EstadosDeConexion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estatuses_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Integrantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaDeIngreso = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FechaDeExpulsion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Integrantes_ChatsGrupales_IdChat",
                        column: x => x.IdChat,
                        principalTable: "ChatsGrupales",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Integrantes_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    IdMensaje = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdEmisor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEliminado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.IdMensaje);
                    table.ForeignKey(
                        name: "FK_Mensajes_ChatBase_IdChat",
                        column: x => x.IdChat,
                        principalTable: "ChatBase",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mensajes_Usuarios_IdEmisor",
                        column: x => x.IdEmisor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChatBase",
                columns: new[] { "IdChat", "CreateDate" },
                values: new object[,]
                {
                    { new Guid("743aab55-c60a-47cb-8e1f-49b1c1cc3a37"), new DateTime(2026, 6, 27, 22, 46, 6, 569, DateTimeKind.Local).AddTicks(9514) },
                    { new Guid("a606e76c-1999-4307-8bb8-931f76b1e7e2"), new DateTime(2026, 6, 27, 22, 46, 6, 569, DateTimeKind.Local).AddTicks(5006) },
                    { new Guid("ceb37a51-6344-4a63-9e2d-9faadd2dc67e"), new DateTime(2026, 6, 27, 22, 46, 6, 568, DateTimeKind.Local).AddTicks(1984) }
                });

            migrationBuilder.InsertData(
                table: "EstadosDeConexion",
                columns: new[] { "Id", "CreateDate", "Description", "IsDeleted", "Sumary" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 27, 22, 46, 6, 565, DateTimeKind.Local).AddTicks(1116), "USUARIO DESCONECTADO", false, "DESCONECTADO" },
                    { 2, new DateTime(2026, 6, 27, 22, 46, 6, 565, DateTimeKind.Local).AddTicks(2330), "USUARIO CONECTADO", false, "CONECTADO" },
                    { 3, new DateTime(2026, 6, 27, 22, 46, 6, 565, DateTimeKind.Local).AddTicks(2334), "USUARIO MODO NO MOLESTAR", false, "NO MOLESTAR" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "CreateDate", "Description", "Sumary" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 27, 22, 46, 6, 562, DateTimeKind.Local).AddTicks(8783), "Usuario administrador", "ADMIN" },
                    { 2, new DateTime(2026, 6, 27, 22, 46, 6, 564, DateTimeKind.Local).AddTicks(9379), "Usuario Desarrollador", "DEV" },
                    { 3, new DateTime(2026, 6, 27, 22, 46, 6, 564, DateTimeKind.Local).AddTicks(9396), "Usuario normal", "USER" }
                });

            migrationBuilder.InsertData(
                table: "ChatsGrupales",
                column: "IdChat",
                value: new Guid("ceb37a51-6344-4a63-9e2d-9faadd2dc67e"));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "CreateDate", "Password", "Rol", "UserName" },
                values: new object[,]
                {
                    { new Guid("4c95c11f-5604-40c1-827a-a49467a683f2"), new DateTime(2026, 6, 27, 22, 46, 6, 567, DateTimeKind.Local).AddTicks(848), "12345678", 1, "Usuario 0" },
                    { new Guid("608cf420-d67a-4b20-83d3-f49cf04e5bc6"), new DateTime(2026, 6, 27, 22, 46, 6, 567, DateTimeKind.Local).AddTicks(2774), "12345678", 3, "Usuario 2" },
                    { new Guid("cff3ee11-b516-4bcb-b98e-47465d5cb7cd"), new DateTime(2026, 6, 27, 22, 46, 6, 567, DateTimeKind.Local).AddTicks(2754), "12345678", 2, "Usuario 1" }
                });

            migrationBuilder.InsertData(
                table: "ChatsIndividuales",
                columns: new[] { "IdChat", "Emisor", "Receptor" },
                values: new object[,]
                {
                    { new Guid("743aab55-c60a-47cb-8e1f-49b1c1cc3a37"), new Guid("cff3ee11-b516-4bcb-b98e-47465d5cb7cd"), new Guid("608cf420-d67a-4b20-83d3-f49cf04e5bc6") },
                    { new Guid("a606e76c-1999-4307-8bb8-931f76b1e7e2"), new Guid("4c95c11f-5604-40c1-827a-a49467a683f2"), new Guid("cff3ee11-b516-4bcb-b98e-47465d5cb7cd") }
                });

            migrationBuilder.InsertData(
                table: "Estatuses",
                columns: new[] { "IdUsuario", "IdEstatus" },
                values: new object[,]
                {
                    { new Guid("4c95c11f-5604-40c1-827a-a49467a683f2"), 1 },
                    { new Guid("608cf420-d67a-4b20-83d3-f49cf04e5bc6"), 1 },
                    { new Guid("cff3ee11-b516-4bcb-b98e-47465d5cb7cd"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Integrantes",
                columns: new[] { "Id", "FechaDeExpulsion", "FechaDeIngreso", "IdChat", "IdUsuario" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 6, 27, 22, 46, 6, 569, DateTimeKind.Local).AddTicks(966), new Guid("ceb37a51-6344-4a63-9e2d-9faadd2dc67e"), new Guid("4c95c11f-5604-40c1-827a-a49467a683f2") },
                    { 2, null, new DateTime(2026, 6, 27, 22, 46, 6, 569, DateTimeKind.Local).AddTicks(1586), new Guid("ceb37a51-6344-4a63-9e2d-9faadd2dc67e"), new Guid("cff3ee11-b516-4bcb-b98e-47465d5cb7cd") },
                    { 3, null, new DateTime(2026, 6, 27, 22, 46, 6, 569, DateTimeKind.Local).AddTicks(1729), new Guid("ceb37a51-6344-4a63-9e2d-9faadd2dc67e"), new Guid("608cf420-d67a-4b20-83d3-f49cf04e5bc6") }
                });

            migrationBuilder.InsertData(
                table: "Mensajes",
                columns: new[] { "IdMensaje", "CreateDate", "FechaEliminado", "FechaModificado", "IdChat", "IdEmisor", "IsDeleted", "Mensaje" },
                values: new object[,]
                {
                    { new Guid("0c8801e1-03a5-496a-92a7-fad6567b6ce8"), new DateTime(2026, 6, 27, 22, 46, 6, 571, DateTimeKind.Local).AddTicks(1060), null, null, new Guid("ceb37a51-6344-4a63-9e2d-9faadd2dc67e"), new Guid("4c95c11f-5604-40c1-827a-a49467a683f2"), false, "Hola a todos" },
                    { new Guid("1f1a31f4-309e-4b0e-a526-0d785f2bac8d"), new DateTime(2026, 6, 27, 22, 46, 6, 571, DateTimeKind.Local).AddTicks(872), null, null, new Guid("743aab55-c60a-47cb-8e1f-49b1c1cc3a37"), new Guid("cff3ee11-b516-4bcb-b98e-47465d5cb7cd"), false, "Hola mundo" },
                    { new Guid("f2ac394c-4fb2-4c89-ae26-6efdcda91dcd"), new DateTime(2026, 6, 27, 22, 46, 6, 570, DateTimeKind.Local).AddTicks(8214), null, null, new Guid("a606e76c-1999-4307-8bb8-931f76b1e7e2"), new Guid("4c95c11f-5604-40c1-827a-a49467a683f2"), false, "Respuesta del mundo :)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatsIndividuales_Emisor",
                table: "ChatsIndividuales",
                column: "Emisor");

            migrationBuilder.CreateIndex(
                name: "IX_ChatsIndividuales_Receptor",
                table: "ChatsIndividuales",
                column: "Receptor");

            migrationBuilder.CreateIndex(
                name: "IX_Estatuses_IdEstatus",
                table: "Estatuses",
                column: "IdEstatus");

            migrationBuilder.CreateIndex(
                name: "IX_Integrantes_IdChat",
                table: "Integrantes",
                column: "IdChat");

            migrationBuilder.CreateIndex(
                name: "IX_Integrantes_IdUsuario",
                table: "Integrantes",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_IdChat",
                table: "Mensajes",
                column: "IdChat");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_IdEmisor",
                table: "Mensajes",
                column: "IdEmisor");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Rol",
                table: "Usuarios",
                column: "Rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatsIndividuales");

            migrationBuilder.DropTable(
                name: "Estatuses");

            migrationBuilder.DropTable(
                name: "Integrantes");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "EstadosDeConexion");

            migrationBuilder.DropTable(
                name: "ChatsGrupales");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "ChatBase");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
