using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    public partial class change_name_ketquaI_to_ketqua1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tham_quyen_II",
                schema: "KNTC",
                table: "Denounces",
                newName: "tham_quyen_2");

            migrationBuilder.RenameColumn(
                name: "tham_quyen_I",
                schema: "KNTC",
                table: "Denounces",
                newName: "tham_quyen_1");

            migrationBuilder.RenameColumn(
                name: "so_qd_II",
                schema: "KNTC",
                table: "Denounces",
                newName: "so_qd_2");

            migrationBuilder.RenameColumn(
                name: "so_qd_I",
                schema: "KNTC",
                table: "Denounces",
                newName: "so_qd_1");

            migrationBuilder.RenameColumn(
                name: "ngay_tra_kq_II",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_tra_kq_2");

            migrationBuilder.RenameColumn(
                name: "ngay_tra_kq_I",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_tra_kq_1");

            migrationBuilder.RenameColumn(
                name: "ngay_khieu_nai_II",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_khieu_nai_2");

            migrationBuilder.RenameColumn(
                name: "ngay_khieu_nai_I",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_khieu_nai_1");

            migrationBuilder.RenameColumn(
                name: "ket_qua_II",
                schema: "KNTC",
                table: "Denounces",
                newName: "ket_qua_2");

            migrationBuilder.RenameColumn(
                name: "ket_qua_I",
                schema: "KNTC",
                table: "Denounces",
                newName: "ket_qua_1");

            migrationBuilder.RenameColumn(
                name: "tham_quyen_II",
                schema: "KNTC",
                table: "Complains",
                newName: "tham_quyen_2");

            migrationBuilder.RenameColumn(
                name: "tham_quyen_I",
                schema: "KNTC",
                table: "Complains",
                newName: "tham_quyen_1");

            migrationBuilder.RenameColumn(
                name: "so_qd_II",
                schema: "KNTC",
                table: "Complains",
                newName: "so_qd_2");

            migrationBuilder.RenameColumn(
                name: "so_qd_I",
                schema: "KNTC",
                table: "Complains",
                newName: "so_qd_1");

            migrationBuilder.RenameColumn(
                name: "ngay_tra_kq_II",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_tra_kq_2");

            migrationBuilder.RenameColumn(
                name: "ngay_tra_kq_I",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_tra_kq_1");

            migrationBuilder.RenameColumn(
                name: "ngay_khieu_nai_II",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_khieu_nai_2");

            migrationBuilder.RenameColumn(
                name: "ngay_khieu_nai_I",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_khieu_nai_1");

            migrationBuilder.RenameColumn(
                name: "ket_qua_II",
                schema: "KNTC",
                table: "Complains",
                newName: "ket_qua_2");

            migrationBuilder.RenameColumn(
                name: "ket_qua_I",
                schema: "KNTC",
                table: "Complains",
                newName: "ket_qua_1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tham_quyen_2",
                schema: "KNTC",
                table: "Denounces",
                newName: "tham_quyen_II");

            migrationBuilder.RenameColumn(
                name: "tham_quyen_1",
                schema: "KNTC",
                table: "Denounces",
                newName: "tham_quyen_I");

            migrationBuilder.RenameColumn(
                name: "so_qd_2",
                schema: "KNTC",
                table: "Denounces",
                newName: "so_qd_II");

            migrationBuilder.RenameColumn(
                name: "so_qd_1",
                schema: "KNTC",
                table: "Denounces",
                newName: "so_qd_I");

            migrationBuilder.RenameColumn(
                name: "ngay_tra_kq_2",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_tra_kq_II");

            migrationBuilder.RenameColumn(
                name: "ngay_tra_kq_1",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_tra_kq_I");

            migrationBuilder.RenameColumn(
                name: "ngay_khieu_nai_2",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_khieu_nai_II");

            migrationBuilder.RenameColumn(
                name: "ngay_khieu_nai_1",
                schema: "KNTC",
                table: "Denounces",
                newName: "ngay_khieu_nai_I");

            migrationBuilder.RenameColumn(
                name: "ket_qua_2",
                schema: "KNTC",
                table: "Denounces",
                newName: "ket_qua_II");

            migrationBuilder.RenameColumn(
                name: "ket_qua_1",
                schema: "KNTC",
                table: "Denounces",
                newName: "ket_qua_I");

            migrationBuilder.RenameColumn(
                name: "tham_quyen_2",
                schema: "KNTC",
                table: "Complains",
                newName: "tham_quyen_II");

            migrationBuilder.RenameColumn(
                name: "tham_quyen_1",
                schema: "KNTC",
                table: "Complains",
                newName: "tham_quyen_I");

            migrationBuilder.RenameColumn(
                name: "so_qd_2",
                schema: "KNTC",
                table: "Complains",
                newName: "so_qd_II");

            migrationBuilder.RenameColumn(
                name: "so_qd_1",
                schema: "KNTC",
                table: "Complains",
                newName: "so_qd_I");

            migrationBuilder.RenameColumn(
                name: "ngay_tra_kq_2",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_tra_kq_II");

            migrationBuilder.RenameColumn(
                name: "ngay_tra_kq_1",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_tra_kq_I");

            migrationBuilder.RenameColumn(
                name: "ngay_khieu_nai_2",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_khieu_nai_II");

            migrationBuilder.RenameColumn(
                name: "ngay_khieu_nai_1",
                schema: "KNTC",
                table: "Complains",
                newName: "ngay_khieu_nai_I");

            migrationBuilder.RenameColumn(
                name: "ket_qua_2",
                schema: "KNTC",
                table: "Complains",
                newName: "ket_qua_II");

            migrationBuilder.RenameColumn(
                name: "ket_qua_1",
                schema: "KNTC",
                table: "Complains",
                newName: "ket_qua_I");
        }
    }
}
