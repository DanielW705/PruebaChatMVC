using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreriaChatMVC.Migrations
{
    /// <inheritdoc />
    public partial class first_migration : Migration
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
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
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
                name: "Estatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEstatus = table.Column<int>(type: "int", nullable: false),
                    UltimaConextion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estatuses_EstadosDeConexion_IdEstatus",
                        column: x => x.IdEstatus,
                        principalTable: "EstadosDeConexion",
                        principalColumn: "Id",
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
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Estatuses_Estatus",
                        column: x => x.Estatus,
                        principalTable: "Estatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_Rol",
                        column: x => x.Rol,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Integrantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaDeIngreso = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FechaDeExpulsion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrantes", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "ChatsGrupales",
                columns: table => new
                {
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdIntegrantes = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_ChatsGrupales_Integrantes_IdIntegrantes",
                        column: x => x.IdIntegrantes,
                        principalTable: "Integrantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatsGrupales_IdIntegrantes",
                table: "ChatsGrupales",
                column: "IdIntegrantes");

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
                name: "IX_Usuarios_Estatus",
                table: "Usuarios",
                column: "Estatus",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Rol",
                table: "Usuarios",
                column: "Rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatsGrupales");

            migrationBuilder.DropTable(
                name: "ChatsIndividuales");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "Integrantes");

            migrationBuilder.DropTable(
                name: "ChatBase");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Estatuses");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "EstadosDeConexion");
        }
    }
}
