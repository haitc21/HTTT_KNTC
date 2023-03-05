using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using static KNTC.Permissions.KNTCPermissions;

namespace KNTC.Complains.Dtos;

public class TepDinhKemHoSoDto : AuditedEntityDto<Guid>
{
    public Guid IdHoSo { get; set; }
    public int GiaiDoan { get; set; }
    public string TenTaiLieu { get; set; }
    public string HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public string ContentType { get; }
    public long? ContentLength { get; }
}
