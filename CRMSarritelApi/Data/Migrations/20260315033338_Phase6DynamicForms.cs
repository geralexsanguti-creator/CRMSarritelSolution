using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class Phase6DynamicForms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EsquemaConfiguracion",
                table: "Productos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatosConfiguracion",
                table: "DetalleVentas",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDpqUIzStOGkmswbfO/nP7IsXPgFU6lrpZxL+fQkGWqm6THcxG+3noDTbZTJ3tq9BQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsquemaConfiguracion",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "DatosConfiguracion",
                table: "DetalleVentas");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELenv38d3XWIGYbPzoLhOHEGhhUVgdd+AWCnmSXErsgnVulvmrDCsb9To8atqEDC9g==");
        }
    }
}
