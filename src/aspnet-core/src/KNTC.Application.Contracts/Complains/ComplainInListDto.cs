using System;

namespace KNTC.Complains;

public class ComplainInListDto
{
    public string MaHoSo { get; set; }
    public string NguoiDeNghi { get; set; }
    public string DiaChiLienHe { get; set; }
    public string DienThoai { get; set; }
    public string TieuDe { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public string BoPhanDangXL { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public DateTime? NgayTraKQ1 { get; set; }
    public LoaiKetQua? KetQua1 { get; set; }
    public DateTime? NgayTraKQ2 { get; set; }
    public LoaiKetQua? KetQua2 { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public TrangThai TrangThai { get; set; }
}