using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEsExtraToComision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsExtra",
                table: "Comisiones",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1000, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1001, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8048));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1002, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1003, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1004, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1005, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8052));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1006, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8052));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1007, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8053));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1008, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1009, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1010, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8055));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1011, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1012, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1013, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1014, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1015, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1016, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1017, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1018, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1019, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1020, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1021, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1022, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1023, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1024, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1025, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8068));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1026, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8068));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1027, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8069));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1028, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1029, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1030, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8071));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1031, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8072));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1032, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8073));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1033, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8073));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1034, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8074));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1035, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8075));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1036, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8076));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1037, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8076));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1038, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1039, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8078));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1040, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8079));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1041, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8081));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1042, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1043, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1044, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1045, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1046, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1047, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8087));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1048, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1049, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1050, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8089));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1051, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1052, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1053, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1054, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1055, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1056, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8093));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPegp8dHLFaICD1Sqyn4vr8lqhXw4I3LWe4mJnFoGPA39yyFQ0+UmCuj0fOj/56a0Q==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEG94S1/yoVC/PqC1Mw7u/2q3xw5X6ByTmfO0yWBelS/ScGQJBpHJ6S9cSUkU1z0m5g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsExtra",
                table: "Comisiones");

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1000, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(1444));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1001, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2457));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1002, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2460));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1003, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2462));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1004, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2464));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1005, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2467));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1006, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2469));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1007, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2471));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1008, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2472));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1009, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2473));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1010, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2476));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1011, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2478));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1012, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2480));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1013, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2482));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1014, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2485));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1015, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2487));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1016, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2489));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1017, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2491));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1018, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2492));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1019, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2494));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1020, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2495));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1021, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2496));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1022, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2497));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1023, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2498));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1024, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2499));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1025, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2500));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1026, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2502));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1027, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2504));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1028, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2506));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1029, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2507));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1030, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2508));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1031, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2510));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1032, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2511));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1033, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2512));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1034, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2516));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1035, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2517));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1036, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2520));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1037, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2521));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1038, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2523));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1039, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2524));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1040, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2525));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1041, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2526));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1042, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2527));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1043, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1044, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2530));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1045, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2531));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1046, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2533));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1047, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2535));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1048, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2538));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1049, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2539));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1050, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2541));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1051, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2542));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1052, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2543));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1053, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2544));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1054, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2546));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1055, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2548));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1056, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2549));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHTzYfnJTbLs0J/uNaxCmnctoFKUJMMnybiF4CCZfzIDol+ppewMY7QAgU557I98lw==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECL3v31tTPVCJpNH5ELH/UNxBsFK6Ibjn6Ya8s41yJZfFY0kFfrRtyTV9bzRCZH8Rg==");
        }
    }
}
