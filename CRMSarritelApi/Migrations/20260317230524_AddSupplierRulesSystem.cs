using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierRulesSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoVenta_Activo",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "TipoVenta_Codigo",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "TipoVenta_Descripcion",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "TipoVenta_Nombre",
                table: "Ventas");

            migrationBuilder.AddColumn<int>(
                name: "TipoVentaId",
                table: "Ventas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Proveedores",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CIF",
                table: "Proveedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailContacto",
                table: "Proveedores",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Proveedores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Proveedores",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombrePlataforma",
                table: "Proveedores",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Proveedores",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Web",
                table: "Proveedores",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "Productos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TiposVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    EsquemaVariablesJson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProveedorTipoVenta",
                columns: table => new
                {
                    ProveedoresId = table.Column<int>(type: "integer", nullable: false),
                    TiposVentaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorTipoVenta", x => new { x.ProveedoresId, x.TiposVentaId });
                    table.ForeignKey(
                        name: "FK_ProveedorTipoVenta_Proveedores_ProveedoresId",
                        column: x => x.ProveedoresId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProveedorTipoVenta_TiposVentas_TiposVentaId",
                        column: x => x.TiposVentaId,
                        principalTable: "TiposVentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReglasComisiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Variable = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Operador = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ValorMin = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorMax = table.Column<decimal>(type: "numeric", nullable: true),
                    TipoRemuneracion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ValorRemuneracion = table.Column<decimal>(type: "numeric", nullable: false),
                    ProveedorId = table.Column<int>(type: "integer", nullable: false),
                    TipoVentaId = table.Column<int>(type: "integer", nullable: true),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
                    Prioridad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReglasComisiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReglasComisiones_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReglasComisiones_TiposVentas_TipoVentaId",
                        column: x => x.TipoVentaId,
                        principalTable: "TiposVentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8834));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8836));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8837));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8838));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8838));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8839));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8840));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8841));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8842));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8843));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8843));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8844));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8845));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8846));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8847));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8847));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8849));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8851));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8852));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8853));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8853));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8854));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8856));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8857));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8858));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8858));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8859));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8861));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8862));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8863));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8864));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8866));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 5, 22, 74, DateTimeKind.Utc).AddTicks(8867));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMMZfGES2dOph5Wb9PQIZBPzftMSmEv2CITrw1IDLzP/LOdYUTRoGsIIdVYJ/3PDXQ==");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_TipoVentaId",
                table: "Ventas",
                column: "TipoVentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProveedorId",
                table: "Productos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorTipoVenta_TiposVentaId",
                table: "ProveedorTipoVenta",
                column: "TiposVentaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReglasComisiones_ProveedorId",
                table: "ReglasComisiones",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReglasComisiones_TipoVentaId",
                table: "ReglasComisiones",
                column: "TipoVentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_ProveedorId",
                table: "Productos",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_TiposVentas_TipoVentaId",
                table: "Ventas",
                column: "TipoVentaId",
                principalTable: "TiposVentas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_ProveedorId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_TiposVentas_TipoVentaId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "ProveedorTipoVenta");

            migrationBuilder.DropTable(
                name: "ReglasComisiones");

            migrationBuilder.DropTable(
                name: "TiposVentas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_TipoVentaId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ProveedorId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "TipoVentaId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "CIF",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "EmailContacto",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "NombrePlataforma",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "Web",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "Productos");

            migrationBuilder.AddColumn<bool>(
                name: "TipoVenta_Activo",
                table: "Ventas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TipoVenta_Codigo",
                table: "Ventas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoVenta_Descripcion",
                table: "Ventas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoVenta_Nombre",
                table: "Ventas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(5243));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6067));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6071));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6071));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6073));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6075));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6076));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6077));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6078));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6079));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6080));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6083));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6084));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6144));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6146));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6147));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6148));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6149));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6151));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6152));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6154));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6155));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6157));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6158));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6161));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6163));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6181));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6183));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6184));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6185));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6186));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6188));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6189));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 20, 24, 23, 270, DateTimeKind.Utc).AddTicks(6191));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBl/HXArh/cEzJ8ilFEbU2uEYb4/wLN4Z0PMLEn/6ZEhUZhMUS/0qF2wBXGqxt4kOA==");
        }
    }
}
