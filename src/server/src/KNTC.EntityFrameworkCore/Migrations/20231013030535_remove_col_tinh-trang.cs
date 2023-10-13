using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class remove_col_tinhtrang : Migration
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
                   ""cccd_cmnd"",
                   ""trang_thai"",
                   ""luu_tru""
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
                   ""cccd_cmnd"",
                   ""trang_thai"",
                   ""luu_tru""
               FROM ""KNTC"".""Denounces"";
           ";
            migrationBuilder.Sql(sql);
            migrationBuilder.Sql(sql);
            string sql2 = @"
            CREATE INDEX IX_Summaries_Id ON ""KNTC"".""Summaries"" (""Id"");
             CREATE INDEX IX_Summaries_LoaiVuViec ON ""KNTC"".""Summaries"" (""loai_vu_viec"");
             CREATE INDEX IX_Summaries_LinhVuc ON ""KNTC"".""Summaries"" (""linh_vuc"");
             CREATE INDEX IX_Summaries_KetQua ON ""KNTC"".""Summaries"" (""ket_qua"");
             CREATE INDEX IX_Summaries_LoaiVuViec_LinhVuc ON ""KNTC"".""Summaries"" (""loai_vu_viec"", ""linh_vuc"");
             CREATE INDEX IX_MaTinhTP ON ""KNTC"".""Summaries"" (""ma_tinh_tp"");
             CREATE INDEX IX_MaQuanHuyen ON ""KNTC"".""Summaries"" (""ma_quan_huyen"");
             CREATE INDEX IX_MaXaPhuongTT ON ""KNTC"".""Summaries"" (""ma_xa_phuong_tt"");
             CREATE INDEX IX_KetQua ON ""KNTC"".""Summaries"" (""ket_qua"");
             CREATE INDEX IX_CongKhai ON ""KNTC"".""Summaries"" (""cong_khai"");
             CREATE INDEX IX_NguoiNopDon ON ""KNTC"".""Summaries"" (""nguoi_nop_don"");
             CREATE INDEX IX_CccdCmnd ON ""KNTC"".""Summaries"" (""cccd_cmnd"");
             CREATE INDEX IX_DienThoai ON ""KNTC"".""Summaries"" (dien_thoai);
             CREATE INDEX IX_ThoiGianTiepNhan ON ""KNTC"".""Summaries"" (""thoi_gian_tiep_nhan"" DESC);
             CREATE INDEX IX_ThoiGianTiepNhan_MaHoSo ON ""KNTC"".""Summaries"" (""thoi_gian_tiep_nhan"" DESC, ""ma_ho_so"");
             CREATE INDEX IX_Summaries_MaHoSo ON ""KNTC"".""Summaries"" (""ma_ho_so"");
             CREATE INDEX IX_Summaries_trangThai ON ""KNTC"".""Summaries"" (""trang_thai"");
             CREATE INDEX IX_Summaries_LuuTru ON ""KNTC"".""Summaries"" (""luu_tru"");
             ";
            migrationBuilder.Sql(sql2);
            migrationBuilder.DropIndex(
                name: "IX_Denounces_tinh_trang",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropIndex(
                name: "IX_Complains_tinh_trang",
                schema: "KNTC",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "tinh_trang",
                schema: "KNTC",
                table: "Denounces");

            migrationBuilder.DropColumn(
                name: "tinh_trang",
                schema: "KNTC",
                table: "Complains");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tinh_trang",
                schema: "KNTC",
                table: "Denounces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "tinh_trang",
                schema: "KNTC",
                table: "Complains",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Denounces_tinh_trang",
                schema: "KNTC",
                table: "Denounces",
                column: "tinh_trang");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_tinh_trang",
                schema: "KNTC",
                table: "Complains",
                column: "tinh_trang");
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
                   ""cccd_cmnd"",
                   ""trang_thai"",
                   ""tinh_trang"",
                   ""luu_tru""
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
                   ""cccd_cmnd"",
                   ""trang_thai"",
                   ""tinh_trang"",
                   ""luu_tru""
               FROM ""KNTC"".""Denounces"";
           ";
            migrationBuilder.Sql(sql);
            migrationBuilder.Sql(sql);
            string sql2 = @"
            CREATE INDEX IX_Summaries_Id ON ""KNTC"".""Summaries"" (""Id"");
             CREATE INDEX IX_Summaries_LoaiVuViec ON ""KNTC"".""Summaries"" (""loai_vu_viec"");
             CREATE INDEX IX_Summaries_LinhVuc ON ""KNTC"".""Summaries"" (""linh_vuc"");
             CREATE INDEX IX_Summaries_KetQua ON ""KNTC"".""Summaries"" (""ket_qua"");
             CREATE INDEX IX_Summaries_LoaiVuViec_LinhVuc ON ""KNTC"".""Summaries"" (""loai_vu_viec"", ""linh_vuc"");
             CREATE INDEX IX_MaTinhTP ON ""KNTC"".""Summaries"" (""ma_tinh_tp"");
             CREATE INDEX IX_MaQuanHuyen ON ""KNTC"".""Summaries"" (""ma_quan_huyen"");
             CREATE INDEX IX_MaXaPhuongTT ON ""KNTC"".""Summaries"" (""ma_xa_phuong_tt"");
             CREATE INDEX IX_KetQua ON ""KNTC"".""Summaries"" (""ket_qua"");
             CREATE INDEX IX_CongKhai ON ""KNTC"".""Summaries"" (""cong_khai"");
             CREATE INDEX IX_NguoiNopDon ON ""KNTC"".""Summaries"" (""nguoi_nop_don"");
             CREATE INDEX IX_CccdCmnd ON ""KNTC"".""Summaries"" (""cccd_cmnd"");
             CREATE INDEX IX_DienThoai ON ""KNTC"".""Summaries"" (dien_thoai);
             CREATE INDEX IX_ThoiGianTiepNhan ON ""KNTC"".""Summaries"" (""thoi_gian_tiep_nhan"" DESC);
             CREATE INDEX IX_ThoiGianTiepNhan_MaHoSo ON ""KNTC"".""Summaries"" (""thoi_gian_tiep_nhan"" DESC, ""ma_ho_so"");
             CREATE INDEX IX_Summaries_MaHoSo ON ""KNTC"".""Summaries"" (""ma_ho_so"");
             CREATE INDEX IX_Summaries_trangThai ON ""KNTC"".""Summaries"" (""trang_thai"");
             CREATE INDEX IX_Summaries_TinhTrang ON ""KNTC"".""Summaries"" (""tinh_trang"");
             CREATE INDEX IX_Summaries_LuuTru ON ""KNTC"".""Summaries"" (""luu_tru"");
             ";
            migrationBuilder.Sql(sql2);
        }
    }
}
