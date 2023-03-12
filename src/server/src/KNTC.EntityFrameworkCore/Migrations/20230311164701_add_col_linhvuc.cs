using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class add_col_linhvuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "linh_vuc",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "linh_vuc",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_linh_vuc",
                schema: "KNTC",
                table: "Denounces",
                column: "linh_vuc");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_linh_vuc",
                schema: "KNTC",
                table: "Complains",
                column: "linh_vuc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Denounces_linh_vuc",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_linh_vuc",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "linh_vuc",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "linh_vuc",
                schema: "KNTC",
                table: "Complains");
        }
    }
}
