using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true,
                comment: "The ImageUrl of the Trainer");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Gyms",
                type: "nvarchar(max)",
                nullable: true,
                comment: "The ImageUrl of the Gym");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: true,
                comment: "The ImageUrl of the Equipment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Equipments");
        }
    }
}
