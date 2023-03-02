using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.HoSos;

public class TepDinhKemHoSo : FullAuditedEntity<Guid>
{
    public Guid IdHoSo { get; set; }
    public HoSo HoSo { get; set; }
    public string TenTaiLieu { get; set; }
    public string HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    public short ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public string FileName { get; }
    public string ContentType { get; }
    public long? ContentLength { get; }

}
