using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class alter_denounce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "cong_khai_KL_QDTC",
                schema: "KNTC",
                table: "Denounces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "gia_han_GQTC_1",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "gia_han_GQTC_2",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_GQTC",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_QDGQTC",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_nhan_TB_KQXLKLTC",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "nguoi_GQTC",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nguoi_bi_to_cao",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "quyet_dinh_dinh_chi_GQTC",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "quyet_dinh_thu_ly_GQTC",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "so_VB_KL_NDTC",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cong_khai_KL_QDTC",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "gia_han_GQTC_1",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "gia_han_GQTC_2",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "ngay_GQTC",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "ngay_QDGQTC",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "ngay_nhan_TB_KQXLKLTC",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "nguoi_GQTC",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "nguoi_bi_to_cao",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "quyet_dinh_dinh_chi_GQTC",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "quyet_dinh_thu_ly_GQTC",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "so_VB_KL_NDTC",
                schema: "KNTC",
                table: "Denounces");
        }
    }
}
