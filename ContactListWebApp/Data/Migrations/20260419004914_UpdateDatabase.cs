using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactListWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("25cd2c6c-e3c7-44da-9396-66b9b742406f"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("4f4e9f06-f15c-49ff-b7cb-31526bf19c1e"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("7ae5234a-050b-45ff-8378-4704c9a9801c"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CategoryId", "CustomSubcategory", "DateOfBirth", "Email", "Name", "Password", "PhoneNumber", "SubcategoryId", "Surname" },
                values: new object[,]
                {
                    { new Guid("25cd2c6c-e3c7-44da-9396-66b9b742406f"), 3, "Driver", new DateOnly(1988, 7, 10), "piotr.lewandowski@example.com", "Piotr", "SecurePass789!$", "+48555666777", null, "Lewandowski" },
                    { new Guid("4f4e9f06-f15c-49ff-b7cb-31526bf19c1e"), 2, null, new DateOnly(1992, 3, 20), "maria.nowak@example.com", "Maria", "SecurePass456!*", "+48987654321", null, "Nowak" },
                    { new Guid("7ae5234a-050b-45ff-8378-4704c9a9801c"), 1, null, new DateOnly(1990, 5, 15), "jan.kowalski@example.com", "Jan", "SecurePass123!+", "+48123456789", 1, "Kowalski" }
                });
        }
    }
}
