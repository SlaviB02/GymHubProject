using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class GymHoursToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClosingHour",
                table: "Gyms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Closing hour of Gym");

            migrationBuilder.AddColumn<int>(
                name: "OpeningHour",
                table: "Gyms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Opening hour of Gym");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingHour",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "OpeningHour",
                table: "Gyms");
        }
    }
}
