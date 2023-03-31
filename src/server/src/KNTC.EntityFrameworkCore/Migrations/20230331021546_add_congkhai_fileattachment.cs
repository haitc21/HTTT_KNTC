using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class add_congkhai_fileattachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "cong_khai",
                schema: "KNTC",
                table: "FileAttachments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cong_khai",
                schema: "KNTC",
                table: "FileAttachments");
        }
    }
}
