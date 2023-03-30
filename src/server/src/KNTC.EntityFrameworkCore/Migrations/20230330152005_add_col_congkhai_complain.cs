using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class add_col_congkhai_complain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cong_khai_KL_QDTC",
                schema: "KNTC",
                table: "Denounces",
                newName: "cong_khai");

            migrationBuilder.AddColumn<bool>(
                name: "cong_khai",
                schema: "KNTC",
                table: "Complains",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cong_khai",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.RenameColumn(
                name: "cong_khai",
                schema: "KNTC",
                table: "Denounces",
                newName: "cong_khai_KL_QDTC");
        }
    }
}
