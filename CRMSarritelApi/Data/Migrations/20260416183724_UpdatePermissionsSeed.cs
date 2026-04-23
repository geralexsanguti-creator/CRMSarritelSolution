using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePermissionsSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Clientes", "Clientes", "clientes:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Clientes", "Clientes", "clientes:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Clientes", "Clientes", "clientes:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Clientes", "Clientes", "clientes:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Usuarios", "Usuarios", "usuarios:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Usuarios", "Usuarios", "usuarios:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Usuarios", "Usuarios", "usuarios:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Usuarios", "Usuarios", "usuarios:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Roles", "Roles", "roles:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Roles", "Roles", "roles:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Roles", "Roles", "roles:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Roles", "Roles", "roles:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Equipos", "Equipos", "equipos:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Equipos", "Equipos", "equipos:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Equipos", "Equipos", "equipos:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Equipos", "Equipos", "equipos:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Fichajes", "Fichajes", "fichajes:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Fichajes", "Fichajes", "fichajes:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Fichajes", "Fichajes", "fichajes:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Fichajes", "Fichajes", "fichajes:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Productos", "Productos", "productos:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Productos", "Productos", "productos:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Productos", "Productos", "productos:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Productos", "Productos", "productos:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Proveedores", "Proveedores", "proveedores:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Proveedores", "Proveedores", "proveedores:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Proveedores", "Proveedores", "proveedores:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Proveedores", "Proveedores", "proveedores:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Tipos-Venta", "Tipos-Venta", "tipos-venta:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Tipos-Venta", "Tipos-Venta", "tipos-venta:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Tipos-Venta", "Tipos-Venta", "tipos-venta:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Tipos-Venta", "Tipos-Venta", "tipos-venta:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Ventas", "Ventas", "ventas:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Ventas", "Ventas", "ventas:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Ventas", "Ventas", "ventas:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Ventas", "Ventas", "ventas:delete" });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "Id", "Descripcion", "Modulo", "Nombre" },
                values: new object[,]
                {
                    { 37, "Permiso para read en el módulo Comisiones", "Comisiones", "comisiones:read" },
                    { 38, "Permiso para create en el módulo Comisiones", "Comisiones", "comisiones:create" },
                    { 39, "Permiso para update en el módulo Comisiones", "Comisiones", "comisiones:update" },
                    { 40, "Permiso para delete en el módulo Comisiones", "Comisiones", "comisiones:delete" },
                    { 41, "Permiso para read en el módulo Contratos", "Contratos", "contratos:read" },
                    { 42, "Permiso para create en el módulo Contratos", "Contratos", "contratos:create" },
                    { 43, "Permiso para update en el módulo Contratos", "Contratos", "contratos:update" },
                    { 44, "Permiso para delete en el módulo Contratos", "Contratos", "contratos:delete" },
                    { 45, "Permiso de visualización en el módulo Dashboard", "Dashboard", "dashboard:read" },
                    { 46, "Permiso para ver ventas de toda la empresa", "Ventas", "ventas:view_all" },
                    { 47, "Permiso para ver comisiones de toda la empresa", "Comisiones", "comisiones:view_all" },
                    { 48, "Permiso para ver fichajes de todo el personal", "Fichajes", "fichajes:view_all" }
                });

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

            migrationBuilder.InsertData(
                table: "RolPermisos",
                columns: new[] { "PermisoId", "RolId", "FechaAsignacion" },
                values: new object[,]
                {
                    { 37, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3280) },
                    { 38, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3281) },
                    { 39, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3281) },
                    { 40, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3282) },
                    { 41, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3282) },
                    { 42, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3283) },
                    { 43, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3283) },
                    { 44, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3283) },
                    { 45, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3284) },
                    { 46, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3284) },
                    { 47, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3285) },
                    { 48, 1, new DateTime(2026, 4, 16, 18, 37, 22, 394, DateTimeKind.Utc).AddTicks(3285) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 37, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 38, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 39, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 40, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 41, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 42, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 43, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 44, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 45, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 46, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 47, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 48, 1 });

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Dashboard", "Dashboard", "dashboard:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Dashboard", "Dashboard", "dashboard:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Dashboard", "Dashboard", "dashboard:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Dashboard", "Dashboard", "dashboard:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Clientes", "Clientes", "clientes:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Clientes", "Clientes", "clientes:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Clientes", "Clientes", "clientes:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Clientes", "Clientes", "clientes:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Usuarios", "Usuarios", "usuarios:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Usuarios", "Usuarios", "usuarios:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Usuarios", "Usuarios", "usuarios:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Usuarios", "Usuarios", "usuarios:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Roles", "Roles", "roles:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Roles", "Roles", "roles:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Roles", "Roles", "roles:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Roles", "Roles", "roles:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Equipos", "Equipos", "equipos:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Equipos", "Equipos", "equipos:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Equipos", "Equipos", "equipos:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Equipos", "Equipos", "equipos:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Fichajes", "Fichajes", "fichajes:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Fichajes", "Fichajes", "fichajes:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Fichajes", "Fichajes", "fichajes:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Fichajes", "Fichajes", "fichajes:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Productos", "Productos", "productos:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Productos", "Productos", "productos:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Productos", "Productos", "productos:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Productos", "Productos", "productos:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Ventas", "Ventas", "ventas:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Ventas", "Ventas", "ventas:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Ventas", "Ventas", "ventas:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Ventas", "Ventas", "ventas:delete" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para read en el módulo Comisiones", "Comisiones", "comisiones:read" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para create en el módulo Comisiones", "Comisiones", "comisiones:create" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para update en el módulo Comisiones", "Comisiones", "comisiones:update" });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Descripcion", "Modulo", "Nombre" },
                values: new object[] { "Permiso para delete en el módulo Comisiones", "Comisiones", "comisiones:delete" });

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(8841));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9800));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9803));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9804));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9805));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9806));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9807));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9807));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9809));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9810));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9811));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9812));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9815));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9816));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9817));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9819));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9821));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9822));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9824));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9826));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9826));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9828));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9829));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9830));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9831));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9833));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9833));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9835));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9835));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9837));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9838));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9839));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 15, 22, 18, 30, 976, DateTimeKind.Utc).AddTicks(9841));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEO/pRIaU0QU8N54sszWYIPQ7QGj+/pC+OXtjHfAq+B5IjY8i4oBuw4zGE8gwgmG2/g==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEITRmUuvIQIIbd3pu5UNOTwhV50kNDkXk360Ux0ufTWRKHvMAdaFa1kbc29Tbm9KEQ==");
        }
    }
}
