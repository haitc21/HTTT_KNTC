using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Denounces;

public class UnitTypeListDto : AuditedEntityDto<Guid>
{
    public string MaHoSo { get; set; }
    public string TieuDe { get; set; }
    public string NguoiDeNghi { get; set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public DateTime NgayTiepNhan { get; set; }
    public DateTime NgayHenTraKQ { get; set; }
    public LoaiKetQua KetQua { get; set; }
}
