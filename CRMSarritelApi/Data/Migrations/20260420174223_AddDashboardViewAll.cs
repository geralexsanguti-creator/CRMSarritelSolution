using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDashboardViewAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "Id", "Descripcion", "Modulo", "Nombre" },
                values: new object[] { 1048, "Permiso para ver la agregación global del dashboard de la empresa entera", "Dashboard", "dashboard:view_all" });

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1000, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(3019));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1001, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4257));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1002, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4261));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1003, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4263));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1004, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4265));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1005, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4267));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1006, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4269));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1007, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4271));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1008, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4272));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1009, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4273));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1010, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4276));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1011, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4278));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1012, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4280));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1013, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4282));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1014, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4283));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1015, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4285));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1016, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4287));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1017, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4289));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1018, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4291));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1019, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4293));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1020, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4294));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1021, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4296));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1022, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4298));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1023, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4299));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1024, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4301));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1025, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4302));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1026, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4303));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1027, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4304));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1028, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4307));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1029, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4308));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1030, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1031, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1032, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1033, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4367));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1034, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4368));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1035, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4370));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1036, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4372));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1037, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4374));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1038, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4375));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1039, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4377));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1040, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4378));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1041, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4380));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1042, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4382));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1043, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4384));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1044, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4385));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1045, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4388));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1046, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4389));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1047, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4390));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJOP2a7HSQKBp2V5MTd9Ty2hfB2oUFSdEBGzOX99h2tzjrXPH51JHzB2/wIY0l0dnQ==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELfEvBDGYPQ/bb4PSsRChcVON5FBU+Zd+O/Wfneg746OJXC5YSG6gW6ygBqHwJtiWA==");

            migrationBuilder.InsertData(
                table: "RolPermisos",
                columns: new[] { "PermisoId", "RolId", "FechaAsignacion" },
                values: new object[] { 1048, 1, new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4391) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1048, 1 });

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1048);

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1000, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 948, DateTimeKind.Utc).AddTicks(9956));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1001, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1002, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(662));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1003, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(662));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1004, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(663));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1005, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(663));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1006, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(664));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1007, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(665));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1008, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(665));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1009, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(666));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1010, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(667));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1011, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(667));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1012, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(668));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1013, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(689));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1014, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(690));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1015, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(690));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1016, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(691));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1017, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(692));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1018, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(693));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1019, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(693));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1020, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(694));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1021, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(695));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1022, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(695));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1023, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(696));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1024, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(697));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1025, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(697));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1026, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(698));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1027, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(699));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1028, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(700));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1029, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(702));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1030, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(703));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1031, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(704));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1032, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(704));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1033, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(705));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1034, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(707));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1035, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(707));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1036, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(708));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1037, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(710));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1038, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(710));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1039, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(711));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1040, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(712));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1041, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(713));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1042, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(715));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1043, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(715));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1044, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(716));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1045, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(717));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1046, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(719));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1047, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 16, 44, 32, 949, DateTimeKind.Utc).AddTicks(720));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIvSZO6RZXcKMQOv/jVXFKvqIImkqFHe5ilJj8K9S0cKKJDChm4fpgRIB5lDIGT7FQ==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBfFKG5mNQSc44YO+BoGYW93If4wqSQ/f0WtDVayxlzvoakJ4FdVZsPi4e0q0/PQOQ==");
        }
    }
}
