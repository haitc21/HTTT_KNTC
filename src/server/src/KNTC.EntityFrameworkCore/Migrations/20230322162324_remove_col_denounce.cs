using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class remove_col_denounce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ket_qua_1",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "ket_qua_2",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "ngay_khieu_nai_1",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "ngay_khieu_nai_2",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "ngay_tra_kq_1",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "ngay_tra_kq_2",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "so_qd_1",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "so_qd_2",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "tham_quyen_1",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "tham_quyen_2",
                schema: "KNTC",
                table: "Denounces");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ket_qua_1",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ket_qua_2",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_khieu_nai_1",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_khieu_nai_2",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_tra_kq_1",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_tra_kq_2",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "so_qd_1",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "so_qd_2",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tham_quyen_1",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tham_quyen_2",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
