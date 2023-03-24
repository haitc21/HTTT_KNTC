using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class rename_nguoitocao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nguoi_de_nghi",
                schema: "KNTC",
                table: "Denounces",
                newName: "nguoi_to_cao");

            migrationBuilder.AlterColumn<int>(
                name: "giai_doan",
                schema: "KNTC",
                table: "FileAttachments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nguoi_to_cao",
                schema: "KNTC",
                table: "Denounces",
                newName: "nguoi_de_nghi");

            migrationBuilder.AlterColumn<int>(
                name: "giai_doan",
                schema: "KNTC",
                table: "FileAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
