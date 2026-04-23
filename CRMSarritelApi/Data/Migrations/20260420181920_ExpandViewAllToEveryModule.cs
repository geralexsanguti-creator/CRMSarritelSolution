using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExpandViewAllToEveryModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1045,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso de visibilidad global sin restricciones de equipo en Clientes", "Clientes", "clientes:view_all" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1046,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso de visibilidad global sin restricciones de equipo en Usuarios", "Usuarios", "usuarios:view_all" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1047,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso de visibilidad global sin restricciones de equipo en Roles", "Roles", "roles:view_all" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1048,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso de visibilidad global sin restricciones de equipo en Equipos", "Equipos", "equipos:view_all" });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "Id", "Descripcion", "Modulo", "Nombre" },
                values: new object[,]
                {
                    { 1049, "Permiso de visibilidad global sin restricciones de equipo en Fichajes", "Fichajes", "fichajes:view_all" },
                    { 1050, "Permiso de visibilidad global sin restricciones de equipo en Productos", "Productos", "productos:view_all" },
                    { 1051, "Permiso de visibilidad global sin restricciones de equipo en Proveedores", "Proveedores", "proveedores:view_all" },
                    { 1052, "Permiso de visibilidad global sin restricciones de equipo en Tipos-Venta", "Tipos-Venta", "tipos-venta:view_all" },
                    { 1053, "Permiso de visibilidad global sin restricciones de equipo en Ventas", "Ventas", "ventas:view_all" },
                    { 1054, "Permiso de visibilidad global sin restricciones de equipo en Comisiones", "Comisiones", "comisiones:view_all" },
                    { 1055, "Permiso de visibilidad global sin restricciones de equipo en Contratos", "Contratos", "contratos:view_all" },
                    { 1056, "Permiso de visibilidad global sin restricciones de equipo en Dashboard", "Dashboard", "dashboard:view_all" }
                });

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

            migrationBuilder.InsertData(
                table: "RolPermisos",
                columns: new[] { "PermisoId", "RolId", "FechaAsignacion" },
                values: new object[,]
                {
                    { 1049, 1, new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2539) },
                    { 1050, 1, new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2541) },
                    { 1051, 1, new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2542) },
                    { 1052, 1, new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2543) },
                    { 1053, 1, new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2544) },
                    { 1054, 1, new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2546) },
                    { 1055, 1, new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2548) },
                    { 1056, 1, new DateTime(2026, 4, 20, 18, 19, 17, 868, DateTimeKind.Utc).AddTicks(2549) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1049, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1050, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1051, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1052, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1053, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1054, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1055, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1056, 1 });

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1049);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1054);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1055);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1056);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1045,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para ver ventas de toda la empresa", "Ventas", "ventas:view_all" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1046,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para ver comisiones de toda la empresa", "Comisiones", "comisiones:view_all" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1047,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para ver fichajes de todo el personal", "Fichajes", "fichajes:view_all" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1048,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para ver la agregación global del dashboard de la empresa entera", "Dashboard", "dashboard:view_all" });

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
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1048, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 20, 17, 42, 20, 815, DateTimeKind.Utc).AddTicks(4391));

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
        }
    }
}
