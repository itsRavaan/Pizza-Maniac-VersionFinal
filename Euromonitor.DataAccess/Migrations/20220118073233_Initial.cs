using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Euromonitor.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AppUserCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserLastActive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserBioInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserIsDeleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    PizzaId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderPizzaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PizzaPurchasePrice = table.Column<double>(type: "float", nullable: false),
                    UnorderOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderIsDeleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PizzaText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PizzaPrice = table.Column<double>(type: "float", nullable: false),
                    PizzaMarketingImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PizzaCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PizzaLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PizzaIsDeleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "AppUserOrder");

            migrationBuilder.DropTable(
                name: "Pizza");
        }
    }
}
