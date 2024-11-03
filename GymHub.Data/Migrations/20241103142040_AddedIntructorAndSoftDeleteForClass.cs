using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIntructorAndSoftDeleteForClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instructor",
                table: "Classes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "The name of the Instructor of the class");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Classes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag for seeing if the entity is deleted or not");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructor",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Classes");
        }
    }
}
