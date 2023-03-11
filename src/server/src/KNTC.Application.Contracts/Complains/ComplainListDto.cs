using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.Complains;

public class ComplainListDto : AuditedEntityDto<Guid>
{
    public string MaHoSo { get; set; }
    public string TieuDe { get; set; }
    public string NguoiDeNghi { get; set; }
    public LinhVuc Linhuc { get; set; }
    public DateTime NgayTiepNhan { get; set; }
    public DateTime NgayHenTraKQ { get; set; }
    public LoaiKetQua KetQua { get; set; }
}
