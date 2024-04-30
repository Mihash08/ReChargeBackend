using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReChargeBackend.Migrations
{
    /// <inheritdoc />
    public partial class IsOverCleanUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_over",
                table: "reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_over",
                table: "reservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
