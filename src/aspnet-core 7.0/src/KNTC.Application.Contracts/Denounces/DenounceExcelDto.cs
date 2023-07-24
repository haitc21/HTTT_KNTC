using System;

namespace KNTC.Denounces;

public class DenounceExcelDto
{
    public string MaHoSo { get; set; }
    public string NguoiNopDon { get; set; }
    public string DienThoai { get; set; }
    public string DiaChiLienHe { get; set; }
    public string NguoiBiToCao { get; set; }
    public string? NguoiGQTC { get; set; }
    public string TieuDe { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public string BoPhanDangXL { get; set; }
    public string KetQua { get; set; }
}