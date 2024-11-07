using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class softDeleteForTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Trainers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("a1e7b2ff-4f1a-4c0a-a0c2-1a5f4f57e6d9"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("a2e8c9ff-4d1b-4d7a-a2f9-4c8e7f5a9b0d"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("a6e7c3ff-8f5c-4c6e-a5f3-8d8f7c6f4e9b"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9b9c"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("b3d6a2ff-5f2c-4c9e-b3f0-5e9f7d2c4a7b"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("b7f2d1ff-9c6d-4d7f-b6f4-9e7d5c8f3b7d"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("c2d5e9ff-3c8e-4d7d-c1f9-4e9f7b8f6c7d"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("c4b7e1ff-6a3d-4b8d-c4f1-6a9e5c3f9b7c"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("c8d5a9ff-0e7a-4e8f-c7f5-0b8f9d5f2a6c"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("d3f6a1ff-5a7b-4e8c-d2f0-5a8f9b4e7d6f"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("d9b1c4ff-1a8b-4c7a-d8f6-1c7e6d5f9b2c"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("e0f2d8ff-2b9c-4d5e-e9f7-2d8f7c4a5b3d"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("e4b2c8ff-6f3a-4b9d-e3f1-6b9f4d5f8c7a"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("f1a7b3ff-3c0a-4e9b-f1f8-3a7f5d2b6e9c"),
                column: "isDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("f5d9e2ff-7c4b-4d5d-f4f2-7c9e4b3f5a7c"),
                column: "isDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Trainers");
        }
    }
}
