using System;

namespace KNTC.Summaries;

public class SummaryDto
{
    public Guid Id { get; set; }
    public string MaHoSo { get; set; }
    public string NguoiNopDon { get; set; }
    public string DienThoai { get; set; }
    public string DiaChiLienHe { get; set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public LinhVuc LinhVuc { get; set; }
    public string TieuDe { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public string BoPhanDangXL { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public string DuLieuToaDo { get; set; }
    public string DuLieuHinhHoc { get; set; }
}