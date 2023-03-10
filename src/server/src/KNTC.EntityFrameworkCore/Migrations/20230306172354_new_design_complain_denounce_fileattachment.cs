using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace KNTC.Migrations
{
    public partial class new_design_complain_denounce_fileattachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ngay_cap_cccd_cmnd",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "noi_cap_cccd_cmnd",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "ngay_cap_cccd_cmnd",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "noi_cap_cccd_cmnd",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.RenameColumn(
                name: "ngay_tiep_nhan",
                schema: "KNTC",
                table: "Denounces",
                newName: "thoi_gian_tiep_nhan");

            migrationBuilder.RenameColumn(
                name: "ngay_hen_tra_kq",
                schema: "KNTC",
                table: "Denounces",
                newName: "thoi_gian_hen_tra_kq");

            migrationBuilder.RenameColumn(
                name: "ngay_tiep_nhan",
                schema: "KNTC",
                table: "Complains",
                newName: "thoi_gian_tiep_nhan");

            migrationBuilder.RenameColumn(
                name: "ngay_hen_tra_kq",
                schema: "KNTC",
                table: "Complains",
                newName: "thoi_gian_hen_tra_kq");

            migrationBuilder.AlterColumn<int>(
                name: "hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<short>(
                name: "giai_doan",
                schema: "KNTC",
                table: "FileAttachments",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<int>(
                name: "xa_thua_dat",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "tinh_thua_dat",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ma_xa_phuong_tt",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ma_tinh_tp",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ma_quan_huyen",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "huyen_thua_dat",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "dien_tich",
                schema: "KNTC",
                table: "Denounces",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "bo_phan_dang_xl",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "xa_thua_dat",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "tinh_thua_dat",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ma_xa_phuong_tt",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ma_tinh_tp",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ma_quan_huyen",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "huyen_thua_dat",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "dien_tich",
                schema: "KNTC",
                table: "Complains",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "bo_phan_dang_xl",
                schema: "KNTC",
                table: "Complains",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "loai_khieu_nai_1",
                schema: "KNTC",
                table: "Complains",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "loai_khieu_nai_2",
                schema: "KNTC",
                table: "Complains",
                type: "smallint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "giai_doan",
                schema: "KNTC",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "bo_phan_dang_xl",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "bo_phan_dang_xl",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "loai_khieu_nai_1",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "loai_khieu_nai_2",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.RenameColumn(
                name: "thoi_gian_tiep_nhan",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_tiep_nhan");

            migrationBuilder.RenameColumn(
                name: "thoi_gian_hen_tra_kq",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_hen_tra_kq");

            migrationBuilder.RenameColumn(
                name: "thoi_gian_tiep_nhan",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_tiep_nhan");

            migrationBuilder.RenameColumn(
                name: "thoi_gian_hen_tra_kq",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_hen_tra_kq");

            migrationBuilder.AlterColumn<string>(
                name: "hinh_thuc",
                schema: "KNTC",
                table: "FileAttachments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "xa_thua_dat",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "tinh_thua_dat",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ma_xa_phuong_tt",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ma_tinh_tp",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ma_quan_huyen",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "huyen_thua_dat",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "dien_tich",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_cap_cccd_cmnd",
                schema: "KNTC",
                table: "Denounces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "noi_cap_cccd_cmnd",
                schema: "KNTC",
                table: "Denounces",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "xa_thua_dat",
                schema: "KNTC",
                table: "Complains",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "tinh_thua_dat",
                schema: "KNTC",
                table: "Complains",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ma_xa_phuong_tt",
                schema: "KNTC",
                table: "Complains",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ma_tinh_tp",
                schema: "KNTC",
                table: "Complains",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ma_quan_huyen",
                schema: "KNTC",
                table: "Complains",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "huyen_thua_dat",
                schema: "KNTC",
                table: "Complains",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "dien_tich",
                schema: "KNTC",
                table: "Complains",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_cap_cccd_cmnd",
                schema: "KNTC",
                table: "Complains",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "noi_cap_cccd_cmnd",
                schema: "KNTC",
                table: "Complains",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
