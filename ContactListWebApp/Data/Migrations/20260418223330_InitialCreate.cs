using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactListWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactSubcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubcategoryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContactCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactSubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactSubcategories_ContactCategories_ContactCategoryId",
                        column: x => x.ContactCategoryId,
                        principalTable: "ContactCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubcategoryId = table.Column<int>(type: "int", nullable: true),
                    CustomSubcategory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ContactCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactSubcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "ContactSubcategories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ContactCategories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Business" },
                    { 2, "Private" },
                    { 3, "Other" }
                });

            migrationBuilder.InsertData(
                table: "ContactSubcategories",
                columns: new[] { "Id", "ContactCategoryId", "SubcategoryName" },
                values: new object[,]
                {
                    { 1, 1, "Boss" },
                    { 2, 1, "Client" },
                    { 3, 1, "Dealer" },
                    { 4, 1, "Partner" },
                    { 5, 1, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CategoryId", "CustomSubcategory", "DateOfBirth", "Email", "Name", "Password", "PhoneNumber", "SubcategoryId", "Surname" },
                values: new object[,]
                {
                    { new Guid("25cd2c6c-e3c7-44da-9396-66b9b742406f"), 3, "Driver", new DateOnly(1988, 7, 10), "piotr.lewandowski@example.com", "Piotr", "SecurePass789!$", "+48555666777", null, "Lewandowski" },
                    { new Guid("4f4e9f06-f15c-49ff-b7cb-31526bf19c1e"), 2, null, new DateOnly(1992, 3, 20), "maria.nowak@example.com", "Maria", "SecurePass456!*", "+48987654321", null, "Nowak" },
                    { new Guid("7ae5234a-050b-45ff-8378-4704c9a9801c"), 1, null, new DateOnly(1990, 5, 15), "jan.kowalski@example.com", "Jan", "SecurePass123!+", "+48123456789", 1, "Kowalski" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SubcategoryId",
                table: "Contacts",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactSubcategories_ContactCategoryId",
                table: "ContactSubcategories",
                column: "ContactCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactSubcategories");

            migrationBuilder.DropTable(
                name: "ContactCategories");
        }
    }
}
