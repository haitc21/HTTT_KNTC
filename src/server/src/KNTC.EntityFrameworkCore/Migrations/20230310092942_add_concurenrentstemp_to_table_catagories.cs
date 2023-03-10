using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class add_concurenrentstemp_to_table_catagories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "UnitTypes",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "UnitTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "Units",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "Units",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "LandTypes",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "LandTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "DocumentTypes",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "DocumentTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "UnitTypes");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "UnitTypes");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "LandTypes");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "LandTypes");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "KNTC",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                schema: "KNTC",
                table: "DocumentTypes");
        }
    }
}
