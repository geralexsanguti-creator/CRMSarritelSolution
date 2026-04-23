using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePermissionsSeed_Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE \"RolPermisos\" CASCADE; TRUNCATE TABLE \"Permisos\" CASCADE;");

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(8847));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9362));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9364));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9365));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9367));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9369));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9372));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9374));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9376));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9377));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9378));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9384));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9386));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9387));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9389));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9392));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9394));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9394));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9396));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9396));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9400));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9402));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9403));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9405));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9406));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9407));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9409));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9410));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9411));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9411));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 37, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9412));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 38, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9413));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 39, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9415));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 40, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9415));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 41, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9417));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 42, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9418));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 43, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9419));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 44, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9420));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 45, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9448));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 46, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9450));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 47, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9451));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 48, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 42, 36, 593, DateTimeKind.Utc).AddTicks(9452));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOneJrEZLhAQFMWzVp+ydKyImdZYPCeOG3vlxWr7PLZB40WCWxZhKHCmGeGm8vJNCA==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAED6OvbSZXELabmu1Bk9SoM3Rdlx0VxbWBt8pVdU7n7xqJSEW32uTGGMYknPCdBi9zw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(2762));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3258));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3258));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3259));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3259));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3260));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3260));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3261));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3261));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3262));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3262));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3262));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3263));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3265));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3266));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3268));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3268));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3269));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3269));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3269));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3270));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3270));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3272));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3274));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3274));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3275));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3276));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3277));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3278));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3278));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3278));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3279));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3279));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3280));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 37, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3280));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 38, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3281));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 39, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3281));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 40, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3282));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 41, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3282));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 42, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 43, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 44, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 45, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3284));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 46, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3284));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 47, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3285));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 48, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3285));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEClpIYIZgGRJ0dkZCeT2ttqf24rHem4wKm9NOyNgK2vpKboUWM0YUAt0LBZB/ab5gA==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOh6xdI+X+jF3D8oqNmbpHnbPko8oHIK8TC6WZZiiOYvOoeT3knlSk0MnZ+3GmL0SA==");
        }
    }
}
