using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class rm_loaivuviec_in_complain_denounce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Denounces_loai_vu_viec",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_loai_vu_viec",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "Complains");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_loai_vu_viec",
                schema: "KNTC",
                table: "Denounces",
                column: "loai_vu_viec");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_loai_vu_viec",
                schema: "KNTC",
                table: "Complains",
                column: "loai_vu_viec");
        }
    }
}
