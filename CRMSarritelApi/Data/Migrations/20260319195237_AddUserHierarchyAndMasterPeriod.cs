using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserHierarchyAndMasterPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Usuarios_Estado_UsuarioId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_Estado_UsuarioId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "Estado_UsuarioId",
                table: "Ventas");

            migrationBuilder.AddColumn<int>(
                name: "SuperiorId",
                table: "Usuarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsPrincipal",
                table: "PeriodosFacturacion",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4168));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4971));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4973));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4974));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4975));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4976));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4977));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4978));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4978));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4979));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4981));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4982));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4982));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4983));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4985));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4987));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4988));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4988));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4989));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4990));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4991));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4991));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4993));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4994));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4995));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4995));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4997));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4998));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(4999));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(5002));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(5004));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(5004));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 3, 19, 19, 52, 35, 117, DateTimeKind.Utc).AddTicks(5005));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "SuperiorId" },
                values: new object[] { "AQAAAAIAAYagAAAAEDMzTpfIOO0wPDMOff2T3cLEUGVq3OFLAfgjBnehLRc7KhBo7gOf+078F43VrXCPkw==", null });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SuperiorId",
                table: "Usuarios",
                column: "SuperiorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_SuperiorId",
                table: "Usuarios",
                column: "SuperiorId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_SuperiorId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_SuperiorId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SuperiorId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EsPrincipal",
                table: "PeriodosFacturacion");

            migrationBuilder.AddColumn<int>(
                name: "Estado_UsuarioId",
                table: "Ventas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Ventas_Estado_UsuarioId",
                table: "Ventas",
                column: "Estado_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Usuarios_Estado_UsuarioId",
                table: "Ventas",
                column: "Estado_UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
