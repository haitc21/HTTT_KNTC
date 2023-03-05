using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Complains.Dtos;

public class ComplainListDto : AuditedEntityDto<Guid>
{
    public string MaHoSo { get; set; }
    public string TieuDe { get; set; }
    public string NguoiDeNghi { get; set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public LinhVuc LinhVuc { get; set; }
    public DateTime NgayTiepNhan { get; set; }
    public DateTime NgayHenTraKQ { get; set; }
    public DateTime NgayTraKQ { get; set; }
    public LoaiKetQua KetQua { get; set; }
}
