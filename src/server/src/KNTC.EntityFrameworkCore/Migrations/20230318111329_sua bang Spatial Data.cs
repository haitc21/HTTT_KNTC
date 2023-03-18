using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class suabangSpatialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OBJECTID",
                schema: "KNTC",
                table: "SpatialData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Quyen",
                schema: "KNTC",
                table: "SpatialData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "So_to_BD",
                schema: "KNTC",
                table: "SpatialData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenToChuc",
                schema: "KNTC",
                table: "SpatialData",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OBJECTID",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "Quyen",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "So_to_BD",
                schema: "KNTC",
                table: "SpatialData");

            migrationBuilder.DropColumn(
                name: "TenToChuc",
                schema: "KNTC",
                table: "SpatialData");
        }
    }
}
