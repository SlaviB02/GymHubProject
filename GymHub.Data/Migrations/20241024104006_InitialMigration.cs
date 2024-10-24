using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of Equipment"),
                    Model = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "The model of the equipment"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "The type of equipment")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of Gym"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "The Name of the Gym"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The Address of the Gym"),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false, comment: "Description of the gym"),
                    OpeningHour = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Opening hour of Gym"),
                    ClosingHour = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Closing hour of Gym")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the Class"),
                    StartTimeAndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Starting time and date of the Class"),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "The duration of the Class"),
                    GymId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the Gym that the class is in")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of Membership"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "The type of Membership"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Starting Date of the Membership"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Phone number of the person"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "First name of the person"),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Last name of the person"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The unique identifier of the User that made the membership"),
                    GymId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the Gym for the membership")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of Review"),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "The title of the Review"),
                    MainBody = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The MainBody of the Review"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The unique identifier of the User that posted the review"),
                    GymId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the Gym that the review is on")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of Trainer"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "First name of Trainer"),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Last name of Trainer"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Phone Number of Trainer"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Email of Trainer"),
                    GymId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the Gym that the trainer is in")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassesUsers",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the Class"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The unique identifier of the User")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassesUsers", x => new { x.ClassId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ClassesUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassesUsers_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_GymId",
                table: "Classes",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassesUsers_UserId",
                table: "ClassesUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GymsEquipments_EquipmentId",
                table: "GymsEquipments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_GymId",
                table: "Memberships",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_UserId",
                table: "Memberships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_GymId",
                table: "Reviews",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_GymId",
                table: "Trainers",
                column: "GymId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassesUsers");

            migrationBuilder.DropTable(
                name: "GymsEquipments");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Gyms");
        }
    }
}
