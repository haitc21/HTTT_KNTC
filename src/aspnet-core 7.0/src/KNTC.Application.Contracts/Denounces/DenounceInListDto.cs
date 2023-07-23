using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.Denounces;

public class DenounceInListDto : AuditedEntityDto<Guid>
{
    public string MaHoSo { get; set; }
    public string NguoiToCao { get; set; }
    public string DienThoai { get; set; }
    public string DiaChiLienHe { get; set; }
    public string NguoiBiToCao { get; set; }
    public DateTime NgayGQTC { get; set; }
    public string TieuDe { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public string BoPhanDangXL { get; set; }
    public bool CongKhai { get; set; }
}