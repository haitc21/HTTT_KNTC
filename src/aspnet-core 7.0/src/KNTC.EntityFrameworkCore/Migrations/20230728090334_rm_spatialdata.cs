using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class rmspatialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpatialData",
                schema: "KNTC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpatialData",
                schema: "KNTC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GeoJson = table.Column<string>(type: "text", nullable: false),
                    OBJECTID = table.Column<double>(type: "double precision", nullable: false),
                    Quyen = table.Column<float>(type: "real", nullable: true),
                    SotoBD = table.Column<string>(name: "So_to_BD", type: "character varying(255)", maxLength: 255, nullable: true),
                    TenToChuc = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpatialData", x => x.Id);
                });
        }
    }
}
