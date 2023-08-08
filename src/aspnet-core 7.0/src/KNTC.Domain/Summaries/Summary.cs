using System;
using Volo.Abp.Domain.Entities;

namespace KNTC.Summaries;

public class Summary
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
    public string BoPhanDangXL { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public string? DuLieuToaDo { get; set; }
    public string? DuLieuHinhHoc { get; set; }
    public string SoThua { get; set; }
    public string ToBanDo { get; set; }
    public int MaTinhTP { get; set; }
    public int MaQuanHuyen { get; set; }
    public int MaXaPhuongTT { get; set; }
    public bool CongKhai { get; set; }
    public string CccdCmnd { get; set; }
}