using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class rmcomplainiddenounceidfileattachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_ComplainId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_DenounceId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_loai_vu_viec_ComplainId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_loai_vu_viec_DenounceId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "ComplainId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "DenounceId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_loai_vu_viec",
                schema: "KNTC",
                table: "FileAttachments",
                column: "loai_vu_viec");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_loai_vu_viec",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ComplainId",
                schema: "KNTC",
                table: "FileAttachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DenounceId",
                schema: "KNTC",
                table: "FileAttachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_ComplainId",
                schema: "KNTC",
                table: "FileAttachments",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_DenounceId",
                schema: "KNTC",
                table: "FileAttachments",
                column: "DenounceId");

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
    }
}