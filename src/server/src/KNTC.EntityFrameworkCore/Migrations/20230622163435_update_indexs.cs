using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class update_indexs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Denounces_thoi_gian_tiep_nhan",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_thoi_gian_tiep_nhan",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Denounces",
                columns: new[] { "thoi_gian_tiep_nhan", "ma_ho_so" });

            migrationBuilder.CreateIndex(
                name: "IX_Complains_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Complains",
                columns: new[] { "thoi_gian_tiep_nhan", "ma_ho_so" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Denounces_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_thoi_gian_tiep_nhan_ma_ho_so",
                schema: "KNTC",
                table: "Complains");

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
    }
}
