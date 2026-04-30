using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMS.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "Department", "Designation", "Email", "FirstName", "JoinDate", "LastName", "Phone", "Salary", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering", "Software Engineer", "priya@example.com", "Priya", new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sharma", "9876543210", 50000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "HR", "HR Manager", "rahul@example.com", "Rahul", new DateTime(2021, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patil", "9123456780", 60000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance", "Accountant", "sneha@example.com", "Sneha", new DateTime(2020, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joshi", "9988776655", 55000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing", "Marketing Lead", "amit@example.com", "Amit", new DateTime(2019, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kulkarni", "9012345678", 65000m, "Inactive", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Operations", "Operations Manager", "neha@example.com", "Neha", new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desai", "9090909090", 70000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering", "Backend Developer", "rohit@example.com", "Rohit", new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mehta", "9876501234", 52000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing", "SEO Specialist", "anjali@example.com", "Anjali", new DateTime(2022, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Verma", "9988001122", 48000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance", "Financial Analyst", "karan@example.com", "Karan", new DateTime(2020, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singh", "9123409876", 58000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "HR", "HR Executive", "pooja@example.com", "Pooja", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nair", "9870012345", 45000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Operations", "Operations Executive", "vikas@example.com", "Vikas", new DateTime(2021, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reddy", "9001122334", 47000m, "Inactive", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance", "Senior Accountant", "meena@example.com", "Meena", new DateTime(2019, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iyer", "9098765432", 62000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering", "Frontend Developer", "arjun@example.com", "Arjun", new DateTime(2022, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kapoor", "9812345670", 51000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing", "Content Strategist", "divya@example.com", "Divya", new DateTime(2021, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agarwal", "9873216540", 49000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Operations", "Logistics Manager", "suresh@example.com", "Suresh", new DateTime(2020, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yadav", "9988771122", 63000m, "Inactive", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "HR", "HR Manager", "kavita@example.com", "Kavita", new DateTime(2018, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shah", "9011223344", 64000m, "Active", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$5WC2JgsDOnVqtc3ZpXMV..l9CBtsOt7wAh19hZnZP5lZXtJzFV6v2", "Admin", "admin" },
                    { 2, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$hd7FYHd/M2N14su7R1MHHebMqCW2Q8zdUoKA1vEyqxA3dGYS4CSUG", "Viewer", "viewer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
