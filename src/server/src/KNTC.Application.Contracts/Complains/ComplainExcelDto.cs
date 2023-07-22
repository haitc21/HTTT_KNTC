using System;

namespace KNTC.Complains;

public class ComplainExcelDto
{
    public string MaHoSo { get; set; }
    public string NguoiNopDon { get; set; }
    public string DiaChiLienHe { get; set; }
    public string DienThoai { get; set; }
    public string TieuDe { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public string BoPhanDangXL { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public DateTime? NgayTraKQ1 { get; set; }
    public string ThamQuyen1 { get; set; }
    public string KetQua1 { get; set; }
    public DateTime? NgayTraKQ2 { get; set; }
    public string ThamQuyen2 { get; set; }
    public string KetQua2 { get; set; }
    public string KetQua { get; set; }
}