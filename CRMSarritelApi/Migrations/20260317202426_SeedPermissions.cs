using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Use raw SQL with ON CONFLICT DO NOTHING so this is idempotent
            migrationBuilder.Sql(@"
                INSERT INTO ""Permisos"" (""Id"", ""Descripcion"", ""Modulo"", ""Nombre"") VALUES
                (1, 'Permiso para read en el módulo Dashboard', 'Dashboard', 'dashboard:read'),
                (2, 'Permiso para create en el módulo Dashboard', 'Dashboard', 'dashboard:create'),
                (3, 'Permiso para update en el módulo Dashboard', 'Dashboard', 'dashboard:update'),
                (4, 'Permiso para delete en el módulo Dashboard', 'Dashboard', 'dashboard:delete'),
                (5, 'Permiso para read en el módulo Clientes', 'Clientes', 'clientes:read'),
                (6, 'Permiso para create en el módulo Clientes', 'Clientes', 'clientes:create'),
                (7, 'Permiso para update en el módulo Clientes', 'Clientes', 'clientes:update'),
                (8, 'Permiso para delete en el módulo Clientes', 'Clientes', 'clientes:delete'),
                (9, 'Permiso para read en el módulo Usuarios', 'Usuarios', 'usuarios:read'),
                (10, 'Permiso para create en el módulo Usuarios', 'Usuarios', 'usuarios:create'),
                (11, 'Permiso para update en el módulo Usuarios', 'Usuarios', 'usuarios:update'),
                (12, 'Permiso para delete en el módulo Usuarios', 'Usuarios', 'usuarios:delete'),
                (13, 'Permiso para read en el módulo Roles', 'Roles', 'roles:read'),
                (14, 'Permiso para create en el módulo Roles', 'Roles', 'roles:create'),
                (15, 'Permiso para update en el módulo Roles', 'Roles', 'roles:update'),
                (16, 'Permiso para delete en el módulo Roles', 'Roles', 'roles:delete'),
                (17, 'Permiso para read en el módulo Equipos', 'Equipos', 'equipos:read'),
                (18, 'Permiso para create en el módulo Equipos', 'Equipos', 'equipos:create'),
                (19, 'Permiso para update en el módulo Equipos', 'Equipos', 'equipos:update'),
                (20, 'Permiso para delete en el módulo Equipos', 'Equipos', 'equipos:delete'),
                (21, 'Permiso para read en el módulo Fichajes', 'Fichajes', 'fichajes:read'),
                (22, 'Permiso para create en el módulo Fichajes', 'Fichajes', 'fichajes:create'),
                (23, 'Permiso para update en el módulo Fichajes', 'Fichajes', 'fichajes:update'),
                (24, 'Permiso para delete en el módulo Fichajes', 'Fichajes', 'fichajes:delete'),
                (25, 'Permiso para read en el módulo Productos', 'Productos', 'productos:read'),
                (26, 'Permiso para create en el módulo Productos', 'Productos', 'productos:create'),
                (27, 'Permiso para update en el módulo Productos', 'Productos', 'productos:update'),
                (28, 'Permiso para delete en el módulo Productos', 'Productos', 'productos:delete'),
                (29, 'Permiso para read en el módulo Ventas', 'Ventas', 'ventas:read'),
                (30, 'Permiso para create en el módulo Ventas', 'Ventas', 'ventas:create'),
                (31, 'Permiso para update en el módulo Ventas', 'Ventas', 'ventas:update'),
                (32, 'Permiso para delete en el módulo Ventas', 'Ventas', 'ventas:delete'),
                (33, 'Permiso para read en el módulo Comisiones', 'Comisiones', 'comisiones:read'),
                (34, 'Permiso para create en el módulo Comisiones', 'Comisiones', 'comisiones:create'),
                (35, 'Permiso para update en el módulo Comisiones', 'Comisiones', 'comisiones:update'),
                (36, 'Permiso para delete en el módulo Comisiones', 'Comisiones', 'comisiones:delete')
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            migrationBuilder.Sql(@"
                INSERT INTO ""RolPermisos"" (""PermisoId"", ""RolId"", ""FechaAsignacion"")
                SELECT p.""Id"", 1, NOW()
                FROM ""Permisos"" p
                WHERE NOT EXISTS (
                    SELECT 1 FROM ""RolPermisos"" rp WHERE rp.""PermisoId"" = p.""Id"" AND rp.""RolId"" = 1
                );
            ");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBl/HXArh/cEzJ8ilFEbU2uEYb4/wLN4Z0PMLEn/6ZEhUZhMUS/0qF2wBXGqxt4kOA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 });

            migrationBuilder.DeleteData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 });

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFQk6hkNa3Iv1s6EuOcwlOy51rPFRmrzhFRaBwA5AuL24oqazUbSbjPd4yHGGIpfLg==");
        }
    }
}
