using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name_Product = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color1_text = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Color1_rgb = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Color2_text = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Color2_rgb = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Color3_text = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Color3_rgb = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Color4_text = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Color4_rgb = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Number = table.Column<double>(type: "float", nullable: false),
                    CategoryParent_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryChild_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Discount_Single = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    Discount_Major = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    Dimensions = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Ordering = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemic = table.Column<bool>(type: "bit", nullable: false),
                    IsProgrammer = table.Column<bool>(type: "bit", nullable: false),
                    IsUndeletable = table.Column<bool>(type: "bit", nullable: false),
                    IsProfilePublic = table.Column<bool>(type: "bit", nullable: false),
                    IsEmailAddressVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsCellPhoneNumberVerified = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    EmailAddressVerificationKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellPhoneNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    CellPhoneNumberVerificationKey = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordering = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
