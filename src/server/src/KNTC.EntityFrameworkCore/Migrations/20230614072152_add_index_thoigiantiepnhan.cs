using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class add_index_thoigiantiepnhan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Denounces_thoi_gian_tiep_nhan",
                schema: "KNTC",
                table: "Denounces",
                column: "thoi_gian_tiep_nhan");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_thoi_gian_tiep_nhan",
                schema: "KNTC",
                table: "Complains",
                column: "thoi_gian_tiep_nhan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Denounces_thoi_gian_tiep_nhan",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_thoi_gian_tiep_nhan",
                schema: "KNTC",
                table: "Complains");
        }
    }
}