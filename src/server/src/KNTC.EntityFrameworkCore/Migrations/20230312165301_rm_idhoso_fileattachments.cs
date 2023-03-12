using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class rm_idhoso_fileattachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_Complains_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_Denounces_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "id_ho_so",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.AddColumn<Guid>(
                name: "ComplainId",
                schema: "KNTC",
                table: "FileAttachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DenounceId",
                schema: "KNTC",
                table: "FileAttachments",
                type: "uniqueidentifier",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_ComplainId",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_DenounceId",
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

            migrationBuilder.AddColumn<Guid>(
                name: "id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                column: "id_ho_so");

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_Complains_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                column: "id_ho_so",
                principalSchema: "KNTC",
                principalTable: "Complains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_Denounces_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                column: "id_ho_so",
                principalSchema: "KNTC",
                principalTable: "Denounces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
