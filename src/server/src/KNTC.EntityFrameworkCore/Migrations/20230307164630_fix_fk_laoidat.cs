using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class fix_fk_laoidat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complains_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Complains");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Denounces_LandTypes_loai_dat",
            //    schema: "KNTC",
            //    table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Denounces_loai_dat",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_loai_dat",
                schema: "KNTC",
                table: "Complains");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Denounces_loai_dat",
                schema: "KNTC",
                table: "Denounces",
                column: "loai_dat");

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
