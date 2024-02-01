using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReChargeBackend.Migrations
{
    /// <inheritdoc />
    public partial class SlotLengthMinutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "length_minutes",
                table: "slot",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "length_minutes",
                table: "slot");
        }
    }
}
