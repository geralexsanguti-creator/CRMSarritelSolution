using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeCantidadNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "Productos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(617));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1451));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1452));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1453));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1454));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1454));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1455));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1456));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1456));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1457));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1458));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1459));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1459));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1460));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1461));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1461));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1463));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1464));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1465));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1465));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1466));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1467));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1467));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1468));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1469));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1469));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1471));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1472));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1472));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1473));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1474));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1474));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1475));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1476));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1476));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 25, 22, 30, 26, 12, DateTimeKind.Utc).AddTicks(1477));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOvxBZRtnpvo6KQQ6P0p/4K/cSj780X4QIZ9GPZ4iFbGvfNhA1lsn0SmeVOuCXMwxQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "Productos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(7924));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8956));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8958));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8959));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8961));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8964));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8966));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8967));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8968));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8969));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8970));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8971));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8972));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8973));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8974));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8976));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8977));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8978));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8979));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8980));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8984));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8985));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8986));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8987));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8989));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8990));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8991));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8992));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8995));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8996));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(8998));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(9000));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(9002));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(9003));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 24, 21, 38, 22, 635, DateTimeKind.Utc).AddTicks(9004));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENhXpo5vq1qAkJJ3reNMEdR9LltWUnhc5w5VKJ4Ar04pG2wEv/H4Gv6JVaeIlMeRUQ==");
        }
    }
}
