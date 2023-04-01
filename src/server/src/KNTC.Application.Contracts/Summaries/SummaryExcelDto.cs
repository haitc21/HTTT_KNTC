using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Summaries;

public class SummaryExcelDto
{
    public string MaHoSo { get; set; }
    public string NguoiNopDon { get; set; }
    public string DienThoai { get; set; }
    public string DiaChiLienHe { get; set; }
    public string LoaiVuViec { get; set; }
    public string LinhVuc { get; set; }
    public string TieuDe { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public string BoPhanDangXL { get; set; }
    public string KetQua { get; set; }

}
