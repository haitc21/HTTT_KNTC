using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class rename_nguoidenghi_nguoitocao_to_nguoinopdon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nguoi_to_cao",
                schema: "KNTC",
                table: "Denounces",
                newName: "nguoi_nop_don");

            migrationBuilder.RenameColumn(
                name: "nguoi_de_nghi",
                schema: "KNTC",
                table: "Complains",
                newName: "nguoi_nop_don");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nguoi_nop_don",
                schema: "KNTC",
                table: "Denounces",
                newName: "nguoi_to_cao");

            migrationBuilder.RenameColumn(
                name: "nguoi_nop_don",
                schema: "KNTC",
                table: "Complains",
                newName: "nguoi_de_nghi");
        }
    }
}
