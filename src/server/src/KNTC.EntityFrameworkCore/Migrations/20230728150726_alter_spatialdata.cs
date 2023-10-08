using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class alterspatialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpatialData",
                schema: "SPATIAL_DATA",
                table: "SpatialData");

            migrationBuilder.RenameTable(
                name: "SpatialData",
                schema: "SPATIAL_DATA",
                newName: "SpatialDatas",
                newSchema: "SPATIAL_DATA");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    schema: "SPATIAL_DATA",
            //    table: "SpatialDatas",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uuid")
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "IdHoSo",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "cccd_cmnd",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "cong_khai",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "dien_thoai",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ket_qua",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "linh_vuc",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ma_ho_so",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ma_quan_huyen",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ma_tinh_tp",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ma_xa_phuong_tt",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "nguoi_nop_don",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "thoi_gian_tiep_nhan",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "tieu_de",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpatialDatas",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpatialDatas",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "IdHoSo",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "cccd_cmnd",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "cong_khai",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "dien_thoai",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "ket_qua",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "linh_vuc",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "ma_ho_so",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "ma_quan_huyen",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "ma_tinh_tp",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "ma_xa_phuong_tt",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "nguoi_nop_don",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "thoi_gian_tiep_nhan",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.DropColumn(
                name: "tieu_de",
                schema: "SPATIAL_DATA",
                table: "SpatialDatas");

            migrationBuilder.RenameTable(
                name: "SpatialDatas",
                schema: "SPATIAL_DATA",
                newName: "SpatialData",
                newSchema: "SPATIAL_DATA");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "Id",
            //    schema: "SPATIAL_DATA",
            //    table: "SpatialData",
            //    type: "uuid",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "integer")
            //    .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpatialData",
                schema: "SPATIAL_DATA",
                table: "SpatialData",
                column: "Id");
        }
    }
}