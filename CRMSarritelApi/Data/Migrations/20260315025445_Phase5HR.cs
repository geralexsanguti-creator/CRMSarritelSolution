using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class Phase5HR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAdjuntos_Comerciales_CreadoPorId",
                table: "ArchivosAdjuntos");

            migrationBuilder.DropForeignKey(
                name: "FK_Comisiones_Comerciales_EmpleadoId",
                table: "Comisiones");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Comerciales_ComercialId",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Comerciales_CreadoPorId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Comerciales_Estado_UsuarioId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Comerciales_UsuarioId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "Comerciales");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "text",
                maxLength: 255,
                nullable: false,
                comment: "BCrypt hash",
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 255,
                oldComment: "BCrypt hash (60 chars)");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Comisiones",
                table: "Usuarios",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Departamento",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaContratacion",
                table: "Usuarios",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Puesto",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalarioBase",
                table: "Usuarios",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fichajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    HoraEntrada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraSalida = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TipoRegistro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HorasExtra = table.Column<double>(type: "double precision", nullable: false),
                    Notas = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fichajes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Activo", "Comisiones", "Departamento", "FechaContratacion", "PasswordHash", "Puesto", "SalarioBase" },
                values: new object[] { true, null, null, null, "AQAAAAIAAYagAAAAEFumkFmeHyP8SI6NVmxxHvjCaov7WIeQm+8THqSveO/QcfPr45phTiNhpuC93Vkjsw==", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Fichajes_UsuarioId",
                table: "Fichajes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAdjuntos_Usuarios_CreadoPorId",
                table: "ArchivosAdjuntos",
                column: "CreadoPorId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comisiones_Usuarios_EmpleadoId",
                table: "Comisiones",
                column: "EmpleadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Usuarios_ComercialId",
                table: "Contratos",
                column: "ComercialId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Usuarios_CreadoPorId",
                table: "Ventas",
                column: "CreadoPorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Usuarios_Estado_UsuarioId",
                table: "Ventas",
                column: "Estado_UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Usuarios_UsuarioId",
                table: "Ventas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAdjuntos_Usuarios_CreadoPorId",
                table: "ArchivosAdjuntos");

            migrationBuilder.DropForeignKey(
                name: "FK_Comisiones_Usuarios_EmpleadoId",
                table: "Comisiones");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Usuarios_ComercialId",
                table: "Contratos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Usuarios_CreadoPorId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Usuarios_Estado_UsuarioId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Usuarios_UsuarioId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "Fichajes");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Comisiones",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FechaContratacion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Puesto",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SalarioBase",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "text",
                maxLength: 255,
                nullable: false,
                comment: "BCrypt hash (60 chars)",
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 255,
                oldComment: "BCrypt hash");

            migrationBuilder.CreateTable(
                name: "Comerciales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comerciales", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGfWtK/PJGQ/s0H5qWH6xn7sSxMyZbmSppkYzJiowBpleqMibQ52qdL2iEguK3SHJg==");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAdjuntos_Comerciales_CreadoPorId",
                table: "ArchivosAdjuntos",
                column: "CreadoPorId",
                principalTable: "Comerciales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comisiones_Comerciales_EmpleadoId",
                table: "Comisiones",
                column: "EmpleadoId",
                principalTable: "Comerciales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Comerciales_ComercialId",
                table: "Contratos",
                column: "ComercialId",
                principalTable: "Comerciales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Comerciales_CreadoPorId",
                table: "Ventas",
                column: "CreadoPorId",
                principalTable: "Comerciales",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Comerciales_Estado_UsuarioId",
                table: "Ventas",
                column: "Estado_UsuarioId",
                principalTable: "Comerciales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Comerciales_UsuarioId",
                table: "Ventas",
                column: "UsuarioId",
                principalTable: "Comerciales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
