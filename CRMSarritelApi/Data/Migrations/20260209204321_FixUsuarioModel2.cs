using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "text",
                maxLength: 255,
                nullable: false,
                comment: "BCrypt hash (60 chars)",
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60,
                oldComment: "BCrypt hash (60 chars)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                comment: "BCrypt hash (60 chars)",
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 255,
                oldComment: "BCrypt hash (60 chars)");
        }
    }
}
