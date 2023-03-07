using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class remove_fk_file_doctype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_FileAttachments_DocumentTypes_hinh_thuc",
            //    schema: "KNTC",
            //    table: "FileAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments",
                column: "hinh_thuc");

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_DocumentTypes_hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments",
                column: "hinh_thuc",
                principalSchema: "KNTC",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
