using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class altersumariesview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"
               DROP MATERIALIZED VIEW ""KNTC"".""Summaries"";
               CREATE MATERIALIZED VIEW ""KNTC"".""Summaries"" AS
               SELECT
                   ""Id"",
                   ""ma_ho_so"",
                   ""nguoi_nop_don"",
                   ""dien_thoai"",
                   ""dia_chi_lien_he"",
                   1 AS ""loai_vu_viec"",
                   ""linh_vuc"",
                   ""tieu_de"",
                   ""thoi_gian_tiep_nhan"",
                   ""thoi_gian_hen_tra_kq"",
                   ""bo_phan_dang_xl"",
                   ""ket_qua"",
                   ""du_lieu_toa_do"",
                   ""du_lieu_hinh_hoc"",
                   ""so_thua"",
                   ""to_ban_do"",
                   ""ma_tinh_tp"",
                   ""ma_quan_huyen"",
                   ""ma_xa_phuong_tt"",
                   ""cong_khai"",
                   ""cccd_cmnd""
               FROM ""KNTC"".""Complains""
               UNION ALL
               SELECT
                   ""Id"",
                   ""ma_ho_so"",
                   ""nguoi_nop_don"",
                   ""dien_thoai"",
                   ""dia_chi_lien_he"",
                   2 AS ""loai_vu_viec"",
                   ""linh_vuc"",
                   ""tieu_de"",
                   ""thoi_gian_tiep_nhan"",
                   ""thoi_gian_hen_tra_kq"",
                   ""bo_phan_dang_xl"",
                   ""ket_qua"",
                   ""du_lieu_toa_do"",
                   ""du_lieu_hinh_hoc"",
                   ""so_thua"",
                   ""to_ban_do"",
                   ""ma_tinh_tp"",
                   ""ma_quan_huyen"",
                   ""ma_xa_phuong_tt"",
                   ""cong_khai"",
                   ""cccd_cmnd""
               FROM ""KNTC"".""Denounces"";
           ";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = @"
               DROP MATERIALIZED VIEW ""KNTC"".""Summaries"";
               CREATE MATERIALIZED VIEW ""KNTC"".""Summaries"" AS
               SELECT
                   ""Id"",
                   ""ma_ho_so"",
                   ""nguoi_nop_don"",
                   ""dien_thoai"",
                   ""dia_chi_lien_he"",
                   1 AS ""loai_vu_viec"",
                   ""linh_vuc"",
                   ""tieu_de"",
                   ""thoi_gian_tiep_nhan"",
                   ""thoi_gian_hen_tra_kq"",
                   ""bo_phan_dang_xl"",
                   ""ket_qua"",
                   ""du_lieu_toa_do"",
                   ""du_lieu_hinh_hoc"",
                   ""so_thua"",
                   ""to_ban_do""
               FROM ""KNTC"".""Complains""
               UNION ALL
               SELECT
                   ""Id"",
                   ""ma_ho_so"",
                   ""nguoi_nop_don"",
                   ""dien_thoai"",
                   ""dia_chi_lien_he"",
                   2 AS ""loai_vu_viec"",
                   ""linh_vuc"",
                   ""tieu_de"",
                   ""thoi_gian_tiep_nhan"",
                   ""thoi_gian_hen_tra_kq"",
                   ""bo_phan_dang_xl"",
                   ""ket_qua"",
                   ""du_lieu_toa_do"",
                   ""du_lieu_hinh_hoc"",
                   ""so_thua"",
                   ""to_ban_do""
               FROM ""KNTC"".""Denounces"";
           ";
            migrationBuilder.Sql(sql);
        }
    }
}