using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class alter_tepdinhkem_add_filename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "file_name",
                schema: "KNTC",
                table: "tep_dinh_kem_ho_so",
                type: "nvarchar(258)",
                maxLength: 258,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_name",
                schema: "KNTC",
                table: "tep_dinh_kem_ho_so");
        }
    }
}
