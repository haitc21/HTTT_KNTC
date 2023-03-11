using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class create_idx_unittypeid_parentid_in_units : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitTypeId_ParentId",
                schema: "KNTC",
                table: "Units",
                columns: new[] { "UnitTypeId", "ParentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Units_UnitTypeId_ParentId",
                schema: "KNTC",
                table: "Units");
        }
    }
}
