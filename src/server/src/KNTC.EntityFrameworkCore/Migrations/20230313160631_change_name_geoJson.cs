using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class change_name_geoJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "geoJson",
                schema: "KNTC",
                table: "SpatialData",
                newName: "GeoJson");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GeoJson",
                schema: "KNTC",
                table: "SpatialData",
                newName: "geoJson");
        }
    }
}
