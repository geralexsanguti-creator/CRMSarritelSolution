using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveJefeEquipoAndAddEsManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Usuarios_JefeEquipoId",
                table: "Equipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_JefeEquipoId",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "JefeEquipoId",
                table: "Equipos");

            migrationBuilder.AddColumn<bool>(
                name: "EsManager",
                table: "UsuarioEquipos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(1459));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2519));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2521));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2523));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2524));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2525));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2526));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2527));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2529));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2533));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2534));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2535));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2536));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2537));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2538));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2539));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2540));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2541));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2541));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2542));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2543));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2544));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2545));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2546));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2547));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2548));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2549));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2550));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2551));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2551));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2553));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2554));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 19, 23, 45, 410, DateTimeKind.Utc).AddTicks(2556));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBH4LxLcqFq0qNlGOfQvJtNryPTZ2kU6YH4CJA3GLXJcrTB48TpMrHQdIHzptnZ3Hw==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENdvceLpAHuRURxZGUMHAp+Zd5o31sKyG49j9Mazq8qNnagyUZBDp+HXIJQu8didVA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsManager",
                table: "UsuarioEquipos");

            migrationBuilder.AddColumn<int>(
                name: "JefeEquipoId",
                table: "Equipos",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6180));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6721));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6724));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6725));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6725));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6726));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6728));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6730));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6731));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6731));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6732));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6733));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6734));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6734));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6735));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6738));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6739));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6741));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6743));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6744));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6745));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6747));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6747));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6749));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6750));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6750));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6751));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6752));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6752));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6753));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6753));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6755));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 14, 18, 8, 0, 641, DateTimeKind.Utc).AddTicks(6756));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEdAUc5AlHWQQ28zbNUciyYThykO1H9rYPFGZsXh/AQvSdT75e9tCciGrHZ7c/cSbQ==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAkZoigrnhSjGSGjN9VEaavFL3wGba6GbH85BEgzZRnctYZH1zKUeh+9s5JnFt9kaw==");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_JefeEquipoId",
                table: "Equipos",
                column: "JefeEquipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Usuarios_JefeEquipoId",
                table: "Equipos",
                column: "JefeEquipoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
