using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class add_loai_vu_viec_fileattachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "FileAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_loai_vu_viec_ComplainId",
                schema: "KNTC",
                table: "FileAttachments",
                columns: new[] { "loai_vu_viec", "ComplainId" });

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_loai_vu_viec_DenounceId",
                schema: "KNTC",
                table: "FileAttachments",
                columns: new[] { "loai_vu_viec", "DenounceId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_loai_vu_viec_ComplainId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_loai_vu_viec_DenounceId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "FileAttachments");
        }
    }
}
