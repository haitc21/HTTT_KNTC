CREATE INDEX idx_Summaries_MaHoSo ON "KNTC"."Summaries" ("ma_ho_so");
CREATE INDEX idx_Summaries_LoaiVuViec ON "KNTC"."Summaries" ("loai_vu_viec");
CREATE INDEX idx_Summaries_LinhVuc ON "KNTC"."Summaries" ("linh_vuc");
CREATE INDEX idx_Summaries_KetQua ON "KNTC"."Summaries" ("ket_qua");
CREATE INDEX idx_Summaries_LoaiVuViec_LinhVuc ON "KNTC"."Summaries" ("loai_vu_viec", "linh_vuc");
CREATE INDEX IX_MaTinhTP ON "KNTC"."Summaries" ("ma_tinh_tp");
CREATE INDEX IX_MaQuanHuyen ON "KNTC"."Summaries" ("ma_quan_huyen");
CREATE INDEX IX_MaXaPhuongTT ON "KNTC"."Summaries" ("ma_xa_phuong_tt");
CREATE INDEX IX_KetQua ON "KNTC"."Summaries" (KetQua);
CREATE INDEX IX_CongKhai ON "KNTC"."Summaries" ("cong_khai");
CREATE INDEX IX_NguoiNopDon ON "KNTC"."Summaries" ("nguoi_nop_don");
CREATE INDEX IX_CccdCmnd ON "KNTC"."Summaries" ("cccd_cmnd");
CREATE INDEX IX_DienThoai ON "KNTC"."Summaries" (dien_thoai);
CREATE INDEX IX_ThoiGianTiepNhan ON "KNTC"."Summaries" ("thoi_gian_tiep_nhan" DESC);
CREATE INDEX IX_ThoiGianTiepNhan_MaHoSo ON "KNTC"."Summaries" ("thoi_gian_tiep_nhan" DESC, "ma_ho_so");



