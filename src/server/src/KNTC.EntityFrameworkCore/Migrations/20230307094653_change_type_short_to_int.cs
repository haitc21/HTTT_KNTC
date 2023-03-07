using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class change_type_short_to_int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "giai_doan",
                schema: "KNTC",
                table: "FileAttachments",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "ket_qua_2",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ket_qua_1",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ket_qua",
                schema: "KNTC",
                table: "Denounces",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "loai_khieu_nai_2",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "loai_khieu_nai_1",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ket_qua_2",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ket_qua_1",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ket_qua",
                schema: "KNTC",
                table: "Complains",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "giai_doan",
                schema: "KNTC",
                table: "FileAttachments",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "Denounces",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "ket_qua_2",
                schema: "KNTC",
                table: "Denounces",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "ket_qua_1",
                schema: "KNTC",
                table: "Denounces",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "ket_qua",
                schema: "KNTC",
                table: "Denounces",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "loai_vu_viec",
                schema: "KNTC",
                table: "Complains",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "loai_khieu_nai_2",
                schema: "KNTC",
                table: "Complains",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "loai_khieu_nai_1",
                schema: "KNTC",
                table: "Complains",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "ket_qua_2",
                schema: "KNTC",
                table: "Complains",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "ket_qua_1",
                schema: "KNTC",
                table: "Complains",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "ket_qua",
                schema: "KNTC",
                table: "Complains",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
