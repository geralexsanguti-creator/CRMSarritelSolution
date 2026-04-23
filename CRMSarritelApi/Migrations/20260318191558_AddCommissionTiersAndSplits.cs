using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCommissionTiersAndSplits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorRemuneracion",
                table: "ReglasComisiones",
                newName: "ValorRemuneracionGross");

            migrationBuilder.RenameColumn(
                name: "TipoRemuneracion",
                table: "ReglasComisiones",
                newName: "TipoRemuneracionGross");

            migrationBuilder.CreateTable(
                name: "PeriodosFacturacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Abierto = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notas = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodosFacturacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductoReglas",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "integer", nullable: false),
                    ReglaComisionId = table.Column<int>(type: "integer", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoReglas", x => new { x.ProductoId, x.ReglaComisionId });
                    table.ForeignKey(
                        name: "FK_ProductoReglas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoReglas_ReglasComisiones_ReglaComisionId",
                        column: x => x.ReglaComisionId,
                        principalTable: "ReglasComisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReglaComisionTiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReglaComisionId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ValorMin = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorMax = table.Column<decimal>(type: "numeric", nullable: true),
                    TipoRemuneracionGross = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ValorRemuneracionGross = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReglaComisionTiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReglaComisionTiers_ReglasComisiones_ReglaComisionId",
                        column: x => x.ReglaComisionId,
                        principalTable: "ReglasComisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReparticionesComision",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    TipoCalculo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    ReglaComisionId = table.Column<int>(type: "integer", nullable: true),
                    TierId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReparticionesComision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReparticionesComision_ReglaComisionTiers_TierId",
                        column: x => x.TierId,
                        principalTable: "ReglaComisionTiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReparticionesComision_ReglasComisiones_ReglaComisionId",
                        column: x => x.ReglaComisionId,
                        principalTable: "ReglasComisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReparticionesComision_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(759));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2366));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2369));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2370));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2375));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2377));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2379));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2380));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2382));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2386));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2388));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2389));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2390));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2392));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2394));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2397));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2400));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2402));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2404));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2406));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2407));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2408));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2409));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2410));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2411));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2412));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2414));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2416));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2418));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2420));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2423));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2425));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2426));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 18, 19, 15, 56, 57, DateTimeKind.Utc).AddTicks(2427));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMJik4Q+XCvDKUt09MXwvL/Q95v4JaCAu/zJDjb4ZA6JHdEZrzyWPAhdsOe3n8kB7A==");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoReglas_ReglaComisionId",
                table: "ProductoReglas",
                column: "ReglaComisionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReglaComisionTiers_ReglaComisionId",
                table: "ReglaComisionTiers",
                column: "ReglaComisionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparticionesComision_ReglaComisionId",
                table: "ReparticionesComision",
                column: "ReglaComisionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparticionesComision_RolId",
                table: "ReparticionesComision",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparticionesComision_TierId",
                table: "ReparticionesComision",
                column: "TierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeriodosFacturacion");

            migrationBuilder.DropTable(
                name: "ProductoReglas");

            migrationBuilder.DropTable(
                name: "ReparticionesComision");

            migrationBuilder.DropTable(
                name: "ReglaComisionTiers");

            migrationBuilder.RenameColumn(
                name: "ValorRemuneracionGross",
                table: "ReglasComisiones",
                newName: "ValorRemuneracion");

            migrationBuilder.RenameColumn(
                name: "TipoRemuneracionGross",
                table: "ReglasComisiones",
                newName: "TipoRemuneracion");

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(943));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1752));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1755));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1758));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1759));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1761));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1761));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1762));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1763));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1764));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1765));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1765));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1766));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1767));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1768));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1769));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1771));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1771));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1772));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1773));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1774));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1775));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1775));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1776));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1777));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1778));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1778));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1779));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1780));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1780));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 17, 23, 41, 24, 400, DateTimeKind.Utc).AddTicks(1781));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHUX9t7zCI5rwdxoCM8bDkfpB4c+7RPdXXYItK3tLJrl4wcQBRXc+4SMwVwst56cbA==");
        }
    }
}
