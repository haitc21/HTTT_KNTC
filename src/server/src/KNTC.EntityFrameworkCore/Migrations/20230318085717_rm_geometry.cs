using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace KNTC.Migrations
{
    public partial class rm_geometry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Geometry",
                schema: "KNTC",
                table: "SpatialData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Geometry>(
                name: "Geometry",
                schema: "KNTC",
                table: "SpatialData",
                type: "geography",
                nullable: true);
        }
    }
}
