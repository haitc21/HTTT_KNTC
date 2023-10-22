using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class set_name_hilo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "EntityFrameworkHiLoSequence");

            migrationBuilder.CreateSequence(
                name: "Sequence-BaseMap",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Sequence-DocumentType",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Sequence-History",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Sequence-LandType",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Sequence-SpatialData",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Sequence-SysConfig",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Sequence-Unit",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Sequence-UnitType",
                incrementBy: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "Sequence-BaseMap");

            migrationBuilder.DropSequence(
                name: "Sequence-DocumentType");

            migrationBuilder.DropSequence(
                name: "Sequence-History");

            migrationBuilder.DropSequence(
                name: "Sequence-LandType");

            migrationBuilder.DropSequence(
                name: "Sequence-SpatialData");

            migrationBuilder.DropSequence(
                name: "Sequence-SysConfig");

            migrationBuilder.DropSequence(
                name: "Sequence-Unit");

            migrationBuilder.DropSequence(
                name: "Sequence-UnitType");

            migrationBuilder.CreateSequence(
                name: "EntityFrameworkHiLoSequence",
                incrementBy: 10);
        }
    }
}