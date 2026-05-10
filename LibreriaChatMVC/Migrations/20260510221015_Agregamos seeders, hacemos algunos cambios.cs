using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibreriaChatMVC.Migrations
{
    /// <inheritdoc />
    public partial class Agregamosseedershacemosalgunoscambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsGrupales_Integrantes_IdIntegrantes",
                table: "ChatsGrupales");

            migrationBuilder.DropIndex(
                name: "IX_ChatsGrupales_IdIntegrantes",
                table: "ChatsGrupales");

            migrationBuilder.DropColumn(
                name: "IdIntegrantes",
                table: "ChatsGrupales");

            migrationBuilder.AddColumn<Guid>(
                name: "IdChat",
                table: "Integrantes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "ChatBase",
                columns: new[] { "IdChat", "CreateDate" },
                values: new object[,]
                {
                    { new Guid("5fa7f8e4-c7af-4e50-b6ba-5dcd825be575"), new DateTime(2026, 5, 10, 16, 10, 14, 750, DateTimeKind.Local).AddTicks(1984) },
                    { new Guid("ceb6abe7-9f22-4360-9a86-f0df2eef2bfc"), new DateTime(2026, 5, 10, 16, 10, 14, 748, DateTimeKind.Local).AddTicks(6552) },
                    { new Guid("f329fda5-6840-40df-a734-77b30c4faff6"), new DateTime(2026, 5, 10, 16, 10, 14, 749, DateTimeKind.Local).AddTicks(8030) }
                });

            migrationBuilder.InsertData(
                table: "EstadosDeConexion",
                columns: new[] { "Id", "CreateDate", "Description", "Sumary" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 10, 16, 10, 14, 747, DateTimeKind.Local).AddTicks(5558), "USUARIO DESCONECTADO", "DESCONECTADO" },
                    { 2, new DateTime(2026, 5, 10, 16, 10, 14, 747, DateTimeKind.Local).AddTicks(6691), "USUARIO CONECTADO", "CONECTADO" },
                    { 3, new DateTime(2026, 5, 10, 16, 10, 14, 747, DateTimeKind.Local).AddTicks(6696), "USUARIO MODO NO MOLESTAR", "NO MOLESTAR" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "CreateDate", "Description", "Sumary" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 10, 16, 10, 14, 745, DateTimeKind.Local).AddTicks(6502), "Usuario administrador", "ADMIN" },
                    { 2, new DateTime(2026, 5, 10, 16, 10, 14, 747, DateTimeKind.Local).AddTicks(3926), "Usuario Desarrollador", "DEV" },
                    { 3, new DateTime(2026, 5, 10, 16, 10, 14, 747, DateTimeKind.Local).AddTicks(3941), "Usuario normal", "USER" }
                });

            migrationBuilder.InsertData(
                table: "ChatsGrupales",
                column: "IdChat",
                value: new Guid("ceb6abe7-9f22-4360-9a86-f0df2eef2bfc"));

            migrationBuilder.InsertData(
                table: "Estatuses",
                columns: new[] { "Id", "IdEstatus" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "CreateDate", "Estatus", "Password", "Rol", "UserName" },
                values: new object[,]
                {
                    { new Guid("69f3eb14-11a6-4d74-b8c7-e2e4e9a09850"), new DateTime(2026, 5, 10, 16, 10, 14, 748, DateTimeKind.Local).AddTicks(2090), 1, "12345678", 1, "Usuario 0" },
                    { new Guid("6c2c9cd7-821b-474a-86b4-252a5ad95943"), new DateTime(2026, 5, 10, 16, 10, 14, 748, DateTimeKind.Local).AddTicks(4821), 2, "12345678", 2, "Usuario 1" },
                    { new Guid("db610712-17f9-40a6-b734-e3dd4bb20e1c"), new DateTime(2026, 5, 10, 16, 10, 14, 748, DateTimeKind.Local).AddTicks(4842), 3, "12345678", 3, "Usuario 2" }
                });

            migrationBuilder.InsertData(
                table: "ChatsIndividuales",
                columns: new[] { "IdChat", "Emisor", "Receptor" },
                values: new object[,]
                {
                    { new Guid("5fa7f8e4-c7af-4e50-b6ba-5dcd825be575"), new Guid("6c2c9cd7-821b-474a-86b4-252a5ad95943"), new Guid("db610712-17f9-40a6-b734-e3dd4bb20e1c") },
                    { new Guid("f329fda5-6840-40df-a734-77b30c4faff6"), new Guid("69f3eb14-11a6-4d74-b8c7-e2e4e9a09850"), new Guid("6c2c9cd7-821b-474a-86b4-252a5ad95943") }
                });

            migrationBuilder.InsertData(
                table: "Integrantes",
                columns: new[] { "Id", "FechaDeExpulsion", "FechaDeIngreso", "IdChat", "IdUsuario" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 5, 10, 16, 10, 14, 749, DateTimeKind.Local).AddTicks(4626), new Guid("ceb6abe7-9f22-4360-9a86-f0df2eef2bfc"), new Guid("69f3eb14-11a6-4d74-b8c7-e2e4e9a09850") },
                    { 2, null, new DateTime(2026, 5, 10, 16, 10, 14, 749, DateTimeKind.Local).AddTicks(5203), new Guid("ceb6abe7-9f22-4360-9a86-f0df2eef2bfc"), new Guid("6c2c9cd7-821b-474a-86b4-252a5ad95943") },
                    { 3, null, new DateTime(2026, 5, 10, 16, 10, 14, 749, DateTimeKind.Local).AddTicks(5267), new Guid("ceb6abe7-9f22-4360-9a86-f0df2eef2bfc"), new Guid("db610712-17f9-40a6-b734-e3dd4bb20e1c") }
                });

            migrationBuilder.InsertData(
                table: "Mensajes",
                columns: new[] { "IdMensaje", "CreateDate", "FechaEliminado", "FechaModificado", "IdChat", "IdEmisor", "IsDeleted", "Mensaje" },
                values: new object[,]
                {
                    { new Guid("3b77d74a-4434-4cfb-9e10-9890aada3b4c"), new DateTime(2026, 5, 10, 16, 10, 14, 751, DateTimeKind.Local).AddTicks(1380), null, null, new Guid("5fa7f8e4-c7af-4e50-b6ba-5dcd825be575"), new Guid("6c2c9cd7-821b-474a-86b4-252a5ad95943"), false, "Hola mundo" },
                    { new Guid("9de40981-058f-4884-8ab5-51f4a8b44171"), new DateTime(2026, 5, 10, 16, 10, 14, 750, DateTimeKind.Local).AddTicks(9033), null, null, new Guid("f329fda5-6840-40df-a734-77b30c4faff6"), new Guid("69f3eb14-11a6-4d74-b8c7-e2e4e9a09850"), false, "Respuesta del mundo :)" },
                    { new Guid("b0da23d0-ddb1-4727-b032-989943d5b8eb"), new DateTime(2026, 5, 10, 16, 10, 14, 751, DateTimeKind.Local).AddTicks(1565), null, null, new Guid("ceb6abe7-9f22-4360-9a86-f0df2eef2bfc"), new Guid("69f3eb14-11a6-4d74-b8c7-e2e4e9a09850"), false, "Hola a todos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Integrantes_IdChat",
                table: "Integrantes",
                column: "IdChat");

            migrationBuilder.AddForeignKey(
                name: "FK_Integrantes_ChatsGrupales_IdChat",
                table: "Integrantes",
                column: "IdChat",
                principalTable: "ChatsGrupales",
                principalColumn: "IdChat",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Integrantes_ChatsGrupales_IdChat",
                table: "Integrantes");

            migrationBuilder.DropIndex(
                name: "IX_Integrantes_IdChat",
                table: "Integrantes");

            migrationBuilder.DeleteData(
                table: "ChatsIndividuales",
                keyColumn: "IdChat",
                keyValue: new Guid("5fa7f8e4-c7af-4e50-b6ba-5dcd825be575"));

            migrationBuilder.DeleteData(
                table: "ChatsIndividuales",
                keyColumn: "IdChat",
                keyValue: new Guid("f329fda5-6840-40df-a734-77b30c4faff6"));

            migrationBuilder.DeleteData(
                table: "EstadosDeConexion",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EstadosDeConexion",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Integrantes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Integrantes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Integrantes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Mensajes",
                keyColumn: "IdMensaje",
                keyValue: new Guid("3b77d74a-4434-4cfb-9e10-9890aada3b4c"));

            migrationBuilder.DeleteData(
                table: "Mensajes",
                keyColumn: "IdMensaje",
                keyValue: new Guid("9de40981-058f-4884-8ab5-51f4a8b44171"));

            migrationBuilder.DeleteData(
                table: "Mensajes",
                keyColumn: "IdMensaje",
                keyValue: new Guid("b0da23d0-ddb1-4727-b032-989943d5b8eb"));

            migrationBuilder.DeleteData(
                table: "ChatBase",
                keyColumn: "IdChat",
                keyValue: new Guid("5fa7f8e4-c7af-4e50-b6ba-5dcd825be575"));

            migrationBuilder.DeleteData(
                table: "ChatBase",
                keyColumn: "IdChat",
                keyValue: new Guid("f329fda5-6840-40df-a734-77b30c4faff6"));

            migrationBuilder.DeleteData(
                table: "ChatsGrupales",
                keyColumn: "IdChat",
                keyValue: new Guid("ceb6abe7-9f22-4360-9a86-f0df2eef2bfc"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: new Guid("69f3eb14-11a6-4d74-b8c7-e2e4e9a09850"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: new Guid("6c2c9cd7-821b-474a-86b4-252a5ad95943"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: new Guid("db610712-17f9-40a6-b734-e3dd4bb20e1c"));

            migrationBuilder.DeleteData(
                table: "ChatBase",
                keyColumn: "IdChat",
                keyValue: new Guid("ceb6abe7-9f22-4360-9a86-f0df2eef2bfc"));

            migrationBuilder.DeleteData(
                table: "Estatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Estatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EstadosDeConexion",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IdChat",
                table: "Integrantes");

            migrationBuilder.AddColumn<int>(
                name: "IdIntegrantes",
                table: "ChatsGrupales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatsGrupales_IdIntegrantes",
                table: "ChatsGrupales",
                column: "IdIntegrantes");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsGrupales_Integrantes_IdIntegrantes",
                table: "ChatsGrupales",
                column: "IdIntegrantes",
                principalTable: "Integrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
