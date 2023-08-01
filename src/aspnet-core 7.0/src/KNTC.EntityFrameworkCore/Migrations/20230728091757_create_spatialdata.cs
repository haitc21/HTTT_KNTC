using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using System;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class createspatialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SPATIAL_DATA");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "SpatialData",
                schema: "SPATIAL_DATA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoaiVuViec = table.Column<int>(type: "integer", nullable: false),
                    DuLieuToaDo = table.Column<Point>(type: "geometry", nullable: true),
                    DuLieuHinhHoc = table.Column<Geometry>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpatialData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpatialData",
                schema: "SPATIAL_DATA");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");
        }
    }
}