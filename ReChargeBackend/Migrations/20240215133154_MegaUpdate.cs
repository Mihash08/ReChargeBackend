using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReChargeBackend.Migrations
{
    /// <inheritdoc />
    public partial class MegaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cancelation_message",
                table: "activity",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cancelation_message",
                table: "activity");
        }
    }
}
