using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class Phase4Reconciliation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Clientes_ClienteId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Comerciales_ComercialId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Productos_ProductoId",
                table: "Contrato");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contrato",
                table: "Contrato");

            migrationBuilder.RenameTable(
                name: "Contrato",
                newName: "Contratos");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_ProductoId",
                table: "Contratos",
                newName: "IX_Contratos_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_ComercialId",
                table: "Contratos",
                newName: "IX_Contratos_ComercialId");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_ClienteId",
                table: "Contratos",
                newName: "IX_Contratos_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contratos",
                table: "Contratos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ArchivosAdjuntos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntidadTipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EntidadId = table.Column<int>(type: "integer", nullable: false),
                    NombreArchivo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TipoMime = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TamanoBytes = table.Column<long>(type: "bigint", nullable: false),
                    ArchivoBytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreadoPorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAdjuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivosAdjuntos_Comerciales_CreadoPorId",
                        column: x => x.CreadoPorId,
                        principalTable: "Comerciales",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGfWtK/PJGQ/s0H5qWH6xn7sSxMyZbmSppkYzJiowBpleqMibQ52qdL2iEguK3SHJg==");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntos_CreadoPorId",
                table: "ArchivosAdjuntos",
                column: "CreadoPorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Clientes_ClienteId",
                table: "Contratos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Comerciales_ComercialId",
                table: "Contratos",
                column: "ComercialId",
                principalTable: "Comerciales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Productos_ProductoId",
                table: "Contratos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Clientes_ClienteId",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Comerciales_ComercialId",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Productos_ProductoId",
                table: "Contratos");

            migrationBuilder.DropTable(
                name: "ArchivosAdjuntos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contratos",
                table: "Contratos");

            migrationBuilder.RenameTable(
                name: "Contratos",
                newName: "Contrato");

            migrationBuilder.RenameIndex(
                name: "IX_Contratos_ProductoId",
                table: "Contrato",
                newName: "IX_Contrato_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Contratos_ComercialId",
                table: "Contrato",
                newName: "IX_Contrato_ComercialId");

            migrationBuilder.RenameIndex(
                name: "IX_Contratos_ClienteId",
                table: "Contrato",
                newName: "IX_Contrato_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contrato",
                table: "Contrato",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIcaXVk77WD/2ATx10wSYuSpKT72y8o36o2wwPzGQQxyOrYjcM21KBK4rMlW+KtetQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Clientes_ClienteId",
                table: "Contrato",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Comerciales_ComercialId",
                table: "Contrato",
                column: "ComercialId",
                principalTable: "Comerciales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Productos_ProductoId",
                table: "Contrato",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }
    }
}
