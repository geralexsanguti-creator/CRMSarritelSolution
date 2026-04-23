using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedDataWithStaticDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UsuarioRoles",
                keyColumns: new[] { "RolId", "UsuarioId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 2, 16, 3, 25, 29, 798, DateTimeKind.Utc).AddTicks(1323));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UsuarioRoles",
                keyColumns: new[] { "RolId", "UsuarioId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 2, 16, 3, 20, 56, 44, DateTimeKind.Utc).AddTicks(3125));
        }
    }
}
