using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSpecificTiersAndAppliedMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "ReparticionesComision",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppliedReglaNombre",
                table: "Comisiones",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppliedTierId",
                table: "Comisiones",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppliedTierNombre",
                table: "Comisiones",
                type: "text",
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
                name: "IX_ReparticionesComision_UsuarioId",
                table: "ReparticionesComision",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReparticionesComision_Usuarios_UsuarioId",
                table: "ReparticionesComision",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReparticionesComision_Usuarios_UsuarioId",
                table: "ReparticionesComision");

            migrationBuilder.DropIndex(
                name: "IX_ReparticionesComision_UsuarioId",
                table: "ReparticionesComision");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "ReparticionesComision");

            migrationBuilder.DropColumn(
                name: "AppliedReglaNombre",
                table: "Comisiones");

            migrationBuilder.DropColumn(
                name: "AppliedTierId",
                table: "Comisiones");

            migrationBuilder.DropColumn(
                name: "AppliedTierNombre",
                table: "Comisiones");

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(1707));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2773));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2776));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2782));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2783));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2789));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2790));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2791));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2792));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2793));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2794));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2795));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2797));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2799));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2801));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2803));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2804));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2806));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2807));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2808));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2809));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2811));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2855));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2856));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2857));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2858));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2860));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2861));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2864));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2865));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2868));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2870));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 13, 18, 57, 31, 729, DateTimeKind.Utc).AddTicks(2872));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMr5nEbo35/E79wyV9h4kY3M2CL6Hf9G00ODFAdD5lY6e0sytRrd1rki+QhYxBt9Zg==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEF3Ge7GzhBchvnqfDNnlV1Brs3/hyvZuKNed4dEUvmUZi590pgJYrZvkx94sNiwknw==");
        }
    }
}
