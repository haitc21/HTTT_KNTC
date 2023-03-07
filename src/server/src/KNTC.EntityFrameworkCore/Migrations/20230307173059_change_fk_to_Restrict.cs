using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class change_fk_to_Restrict : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_DocumentTypes_hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_UnitTypes_UnitTypeId",
                schema: "KNTC",
                table: "Units");

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_loai_dat",
                schema: "KNTC",
                table: "Denounces",
                column: "loai_dat");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_loai_dat",
                schema: "KNTC",
                table: "Complains",
                column: "loai_dat");

            migrationBuilder.AddForeignKey(
                name: "FK_Complains_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Complains",
                column: "loai_dat",
                principalSchema: "KNTC",
                principalTable: "LandTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Denounces_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Denounces",
                column: "loai_dat",
                principalSchema: "KNTC",
                principalTable: "LandTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_DocumentTypes_hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments",
                column: "hinh_thuc",
                principalSchema: "KNTC",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_UnitTypes_UnitTypeId",
                schema: "KNTC",
                table: "Units",
                column: "UnitTypeId",
                principalSchema: "KNTC",
                principalTable: "UnitTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complains_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropForeignKey(
                name: "FK_Denounces_LandTypes_loai_dat",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_Complains_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_Denounces_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_DocumentTypes_hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_UnitTypes_UnitTypeId",
                schema: "KNTC",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Denounces_loai_dat",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_loai_dat",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_Complains_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                column: "id_ho_so",
                principalSchema: "KNTC",
                principalTable: "Complains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_Denounces_id_ho_so",
                schema: "KNTC",
                table: "FileAttachments",
                column: "id_ho_so",
                principalSchema: "KNTC",
                principalTable: "Denounces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_DocumentTypes_hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments",
                column: "hinh_thuc",
                principalSchema: "KNTC",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_UnitTypes_UnitTypeId",
                schema: "KNTC",
                table: "Units",
                column: "UnitTypeId",
                principalSchema: "KNTC",
                principalTable: "UnitTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
