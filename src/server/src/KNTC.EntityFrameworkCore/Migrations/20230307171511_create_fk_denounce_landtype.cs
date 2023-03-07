using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class create_fk_denounce_landtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complains_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropIndex(
                name: "IX_Complains_loai_dat",
                schema: "KNTC",
                table: "Complains");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denounces_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Denounces_loai_dat",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_loai_dat",
                schema: "KNTC",
                table: "Complains",
                column: "loai_dat");

            migrationBuilder.AddForeignKey(
                name: "FK_Complains_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Complains",
                column: "loai_dat",
                principalSchema: "KNTC",
                principalTable: "LandTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
