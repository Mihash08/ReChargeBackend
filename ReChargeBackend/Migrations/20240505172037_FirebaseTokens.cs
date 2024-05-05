using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReChargeBackend.Migrations
{
    /// <inheritdoc />
    public partial class FirebaseTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "firebase_token",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "firebase_token",
                table: "admin_users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "firebase_token",
                table: "users");

            migrationBuilder.DropColumn(
                name: "firebase_token",
                table: "admin_users");
        }
    }
}
