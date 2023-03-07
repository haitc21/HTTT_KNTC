using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class rm_fk_denounce_landtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denounces_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Denounces_loai_dat",
                schema: "KNTC",
                table: "Denounces");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Denounces_loai_dat",
                schema: "KNTC",
                table: "Denounces",
                column: "loai_dat");

            migrationBuilder.AddForeignKey(
                name: "FK_Denounces_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Denounces",
                column: "loai_dat",
                principalSchema: "KNTC",
                principalTable: "LandTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
