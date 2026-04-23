using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class Phase9RBAC_Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEERJNRH4INXzufUzbttLkSukOkSGXTnPMvwPEq3AbF/h9ynaLGwlVEl+EQLud8Mvuw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAuzu4L+AXbEUuwxJ+STVmot4zytJlq5A801ysr+AGl8ekD8bNGsCyYTtdT+hKAZUg==");
        }
    }
}
