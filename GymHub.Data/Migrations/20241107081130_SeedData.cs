using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gyms",
                columns: new[] { "Id", "Address", "ClosingHour", "Description", "ImageUrl", "IsDeleted", "Name", "OpeningHour" },
                values: new object[,]
                {
                    { new Guid("a3b2c1d4-e8f9-4a02-9b3c-5d2e9f7e1c2f"), "321 Wellness Way, Calm Town, CT 67890", 20, "A tranquil fitness space with yoga, Pilates, and meditation classes, along with general fitness facilities.", "/images/MegaGym.jpg", false, "Zen Fitness Center", 7 },
                    { new Guid("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"), "456 Strength Ave, Lift Town, LT 98765", 23, "An exclusive gym focusing on strength training, bodybuilding, and powerlifting.", "/images/VitalSport.jpeg", false, "Powerhouse Fitness", 5 },
                    { new Guid("b7c3d8e2-f5a4-4b23-910f-6c9d2e3a7f4e"), "101 Summit St, Peakville, PV 34567", 22, "A high-energy gym specializing in athletic performance and high-intensity interval training.", "/images/GoldsGym.webp", false, "Gold's Gym", 6 },
                    { new Guid("c9d1e7f6-b4a3-4c87-92d2-8e1c4f9b2d7e"), "555 Flex Rd, Movement City, MC 11223", 23, "A versatile gym with a mix of fitness options, including weightlifting, dance, and mobility classes.", "/images/DefaultGym.jfif", false, "Flex & Flow", 5 },
                    { new Guid("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"), "123 Fitness St, Muscle City, MC 54321", 22, "A fully equipped gym offering various workout zones, personal training, and group classes.", "/images/DefaultGym.jfif", false, "Iron Paradise Gym", 6 },
                    { new Guid("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"), "789 Cardio Blvd, Enduro City, EC 12345", 21, "A cardio-focused gym with a wide variety of machines and group cardio classes.", "/images/PlanetFitness.jfif", false, "Planet Fitness", 5 }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Duration", "GymId", "Instructor", "Name", "StartTimeAndDate", "isDeleted" },
                values: new object[,]
                {
                    { new Guid("a1e4b2ff-4d1a-4c2a-a0b2-1f5f4f57e6a9"), 60, new Guid("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"), "Sarah Lee", "Yoga Basics", new DateTime(2023, 11, 7, 8, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a3c9e2ff-4f2a-4d7a-a2b9-4b8f5d2e7c6e"), 50, new Guid("b7c3d8e2-f5a4-4b23-910f-6c9d2e3a7f4e"), "Jessica Parker", "Kettlebell Workout", new DateTime(2023, 11, 8, 12, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a7f3c2ff-8b5d-4d5e-a6c3-8e9d6c7f4e8d"), 60, new Guid("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"), "Rachel Green", "CrossFit", new DateTime(2023, 11, 7, 17, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9c8d"), 45, new Guid("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"), "Mike Turner", "HIIT Training", new DateTime(2023, 11, 7, 9, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b4d1f1ff-5b3c-4d9d-b3f0-5c9f7d3e4b7c"), 55, new Guid("b7c3d8e2-f5a4-4b23-910f-6c9d2e3a7f4e"), "Eric James", "Functional Fitness", new DateTime(2023, 11, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b8d1e8ff-9c6d-4a7f-b9d4-9f7e4d3f5a6d"), 45, new Guid("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"), "Jake Lewis", "Spinning", new DateTime(2023, 11, 7, 18, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("c3d5e1ff-3a9c-4f3d-c1b4-4e7f6b8f6d8e"), 50, new Guid("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"), "Emily Chen", "Zumba Dance", new DateTime(2023, 11, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("c5e7a2ff-6c4d-4c8d-c4b1-6a8e5f2d9c7a"), 60, new Guid("b7c3d8e2-f5a4-4b23-910f-6c9d2e3a7f4e"), "Olivia Smith", "Dance Cardio", new DateTime(2023, 11, 8, 15, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("c9e5a9ff-0d7c-4c8e-c7f6-0b8f5e6d3a6e"), 50, new Guid("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"), "Sophie King", "Boxing Basics", new DateTime(2023, 11, 7, 20, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("d0a4b1ff-1f8c-4b6d-d8c5-1c6f5d4f7e7f"), 60, new Guid("a3b2c1d4-e8f9-4a02-9b3c-5d2e9f7e1c2f"), "William Scott", "Aerobics", new DateTime(2023, 11, 8, 8, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("d4a8f2ff-5c4a-4d7d-d2c0-5f9e4b4d7f6f"), 60, new Guid("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"), "John Adams", "Strength Training", new DateTime(2023, 11, 7, 12, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("d6f1b8ff-7e5f-4f7f-d6c4-7d9f8e4a5b6c"), 30, new Guid("c9d1e7f6-b4a3-4c87-92d2-8e1c4f9b2d7e"), "Chris Reed", "Morning Run", new DateTime(2023, 11, 9, 6, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("e1d7c4ff-2e9d-4d7f-e9b6-2d8e6d3a5f4e"), 60, new Guid("a3b2c1d4-e8f9-4a02-9b3c-5d2e9f7e1c2f"), "Linda Brown", "Advanced Yoga", new DateTime(2023, 11, 8, 9, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("e5b1c3ff-6d3b-4f5e-e3a1-6c8e5d6f9c7b"), 55, new Guid("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"), "Anna Walker", "Pilates Core", new DateTime(2023, 11, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("e7a2c4ff-8b6c-4d8e-e7d5-8e9d4b2f7a6e"), 60, new Guid("c9d1e7f6-b4a3-4c87-92d2-8e1c4f9b2d7e"), "Vanessa Miller", "Strength Circuit", new DateTime(2023, 11, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("f2a8d3ff-3f1e-4a8b-f1a7-3a7e4b3f5a8d"), 45, new Guid("a3b2c1d4-e8f9-4a02-9b3c-5d2e9f7e1c2f"), "Michael Taylor", "TRX Training", new DateTime(2023, 11, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("f6d9e4ff-7e2c-4b6e-f4b2-7d7f8b5f8a7c"), 40, new Guid("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"), "Tommy Lee", "Cardio Blast", new DateTime(2023, 11, 7, 15, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("f8b3d1ff-9c8d-4e7a-f9c6-9f8e3d5a6c7b"), 40, new Guid("c9d1e7f6-b4a3-4c87-92d2-8e1c4f9b2d7e"), "David Roberts", "Core Stability", new DateTime(2023, 11, 9, 11, 30, 0, 0, DateTimeKind.Unspecified), false }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Email", "FirstName", "GymId", "ImageUrl", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("a1e7b2ff-4f1a-4c0a-a0c2-1a5f4f57e6d9"), "johndoe@example.com", "John", new Guid("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"), "/images/DefaultTrainer.jfif", "Doe", "+12345678901" },
                    { new Guid("a2e8c9ff-4d1b-4d7a-a2f9-4c8e7f5a9b0d"), "ellataylor@example.com", "Ella", new Guid("b7c3d8e2-f5a4-4b23-910f-6c9d2e3a7f4e"), "/images/Ka.jfif", "Taylor", "+12225558888" },
                    { new Guid("a6e7c3ff-8f5c-4c6e-a5f3-8d8f7c6f4e9b"), "meganmartinez@example.com", "Megan", new Guid("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"), "/images/Ka.jfif", "Martinez", "+12223334444" },
                    { new Guid("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9b9c"), "janesmith@example.com", "Jane", new Guid("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"), "/images/Ka.jfif", "Smith", "+19876543210" },
                    { new Guid("b3d6a2ff-5f2c-4c9e-b3f0-5e9f7d2c4a7b"), "lucasmoore@example.com", "Lucas", new Guid("c9d1e7f6-b4a3-4c87-92d2-8e1c4f9b2d7e"), "/images/Dough.jfif", "Moore", "+19994441111" },
                    { new Guid("b7f2d1ff-9c6d-4d7f-b6f4-9e7d5c8f3b7d"), "olivergarcia@example.com", "Oliver", new Guid("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"), "/images/Dough.jfif", "Garcia", "+15556667777" },
                    { new Guid("c2d5e9ff-3c8e-4d7d-c1f9-4e9f7b8f6c7d"), "alicejohnson@example.com", "Alice", new Guid("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"), "/images/Ka.jfif", "Johnson", "+11112223333" },
                    { new Guid("c4b7e1ff-6a3d-4b8d-c4f1-6a9e5c3f9b7c"), "avajackson@example.com", "Ava", new Guid("c9d1e7f6-b4a3-4c87-92d2-8e1c4f9b2d7e"), "/images/Ann.jfif", "Jackson", "+13335557777" },
                    { new Guid("c8d5a9ff-0e7a-4e8f-c7f5-0b8f9d5f2a6c"), "sophiamiller@example.com", "Sophia", new Guid("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"), "/images/Ann.jfif", "Miller", "+18889991111" },
                    { new Guid("d3f6a1ff-5a7b-4e8c-d2f0-5a8f9b4e7d6f"), "bobwilliams@example.com", "Bob", new Guid("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"), "/images/John.jfif", "Williams", "+14445556666" },
                    { new Guid("d9b1c4ff-1a8b-4c7a-d8f6-1c7e6d5f9b2c"), "liamwilson@example.com", "Liam", new Guid("a3b2c1d4-e8f9-4a02-9b3c-5d2e9f7e1c2f"), "/images/Paul.png", "Wilson", "+19992224455" },
                    { new Guid("e0f2d8ff-2b9c-4d5e-e9f7-2d8f7c4a5b3d"), "emmaanderson@example.com", "Emma", new Guid("a3b2c1d4-e8f9-4a02-9b3c-5d2e9f7e1c2f"), "/images/Ann.jfif", "Anderson", "+14445557777" },
                    { new Guid("e4b2c8ff-6f3a-4b9d-e3f1-6b9f4d5f8c7a"), "evebrown@example.com", "Eve", new Guid("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"), "/images/Ann.jfif", "Brown", "+17778889999" },
                    { new Guid("f1a7b3ff-3c0a-4e9b-f1f8-3a7f5d2b6e9c"), "jamesthomas@example.com", "James", new Guid("b7c3d8e2-f5a4-4b23-910f-6c9d2e3a7f4e"), "/images/DefaultTrainer.jfif", "Thomas", "+17773336666" },
                    { new Guid("f5d9e2ff-7c4b-4d5d-f4f2-7c9e4b3f5a7c"), "charliedavis@example.com", "Charlie", new Guid("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"), "/images/Steven.webp", "Davis", "+19991112222" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("a1e4b2ff-4d1a-4c2a-a0b2-1f5f4f57e6a9"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("a3c9e2ff-4f2a-4d7a-a2b9-4b8f5d2e7c6e"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("a7f3c2ff-8b5d-4d5e-a6c3-8e9d6c7f4e8d"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9c8d"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("b4d1f1ff-5b3c-4d9d-b3f0-5c9f7d3e4b7c"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("b8d1e8ff-9c6d-4a7f-b9d4-9f7e4d3f5a6d"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("c3d5e1ff-3a9c-4f3d-c1b4-4e7f6b8f6d8e"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("c5e7a2ff-6c4d-4c8d-c4b1-6a8e5f2d9c7a"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("c9e5a9ff-0d7c-4c8e-c7f6-0b8f5e6d3a6e"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("d0a4b1ff-1f8c-4b6d-d8c5-1c6f5d4f7e7f"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("d4a8f2ff-5c4a-4d7d-d2c0-5f9e4b4d7f6f"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("d6f1b8ff-7e5f-4f7f-d6c4-7d9f8e4a5b6c"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("e1d7c4ff-2e9d-4d7f-e9b6-2d8e6d3a5f4e"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("e5b1c3ff-6d3b-4f5e-e3a1-6c8e5d6f9c7b"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("e7a2c4ff-8b6c-4d8e-e7d5-8e9d4b2f7a6e"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("f2a8d3ff-3f1e-4a8b-f1a7-3a7e4b3f5a8d"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("f6d9e4ff-7e2c-4b6e-f4b2-7d7f8b5f8a7c"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("f8b3d1ff-9c8d-4e7a-f9c6-9f8e3d5a6c7b"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("a1e7b2ff-4f1a-4c0a-a0c2-1a5f4f57e6d9"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("a2e8c9ff-4d1b-4d7a-a2f9-4c8e7f5a9b0d"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("a6e7c3ff-8f5c-4c6e-a5f3-8d8f7c6f4e9b"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9b9c"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("b3d6a2ff-5f2c-4c9e-b3f0-5e9f7d2c4a7b"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("b7f2d1ff-9c6d-4d7f-b6f4-9e7d5c8f3b7d"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("c2d5e9ff-3c8e-4d7d-c1f9-4e9f7b8f6c7d"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("c4b7e1ff-6a3d-4b8d-c4f1-6a9e5c3f9b7c"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("c8d5a9ff-0e7a-4e8f-c7f5-0b8f9d5f2a6c"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("d3f6a1ff-5a7b-4e8c-d2f0-5a8f9b4e7d6f"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("d9b1c4ff-1a8b-4c7a-d8f6-1c7e6d5f9b2c"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("e0f2d8ff-2b9c-4d5e-e9f7-2d8f7c4a5b3d"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("e4b2c8ff-6f3a-4b9d-e3f1-6b9f4d5f8c7a"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("f1a7b3ff-3c0a-4e9b-f1f8-3a7f5d2b6e9c"));

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: new Guid("f5d9e2ff-7c4b-4d5d-f4f2-7c9e4b3f5a7c"));

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: new Guid("a3b2c1d4-e8f9-4a02-9b3c-5d2e9f7e1c2f"));

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: new Guid("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"));

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: new Guid("b7c3d8e2-f5a4-4b23-910f-6c9d2e3a7f4e"));

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: new Guid("c9d1e7f6-b4a3-4c87-92d2-8e1c4f9b2d7e"));

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: new Guid("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"));

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: new Guid("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"));
        }
    }
}
