using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class createidhosofileattachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                column: "id_ho_so");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_loai_vu_viec_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                columns: new[] { "loai_vu_viec", "id_ho_so" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_loai_vu_viec_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "id_ho_so",
                schema: "KNTC",
                table: "FileAttachments");
        }
    }
}