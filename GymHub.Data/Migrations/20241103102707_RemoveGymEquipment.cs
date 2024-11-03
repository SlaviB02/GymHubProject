using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGymEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymsEquipments");

            migrationBuilder.DropTable(
                name: "Equipments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of Equipment"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "The ImageUrl of the Equipment"),
                    Model = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "The model of the equipment"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "The type of equipment")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GymsEquipments",
                columns: table => new
                {
                    GymId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the Gym"),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the Equipment")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymsEquipments", x => new { x.GymId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_GymsEquipments_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GymsEquipments_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymsEquipments_EquipmentId",
                table: "GymsEquipments",
                column: "EquipmentId");
        }
    }
}
