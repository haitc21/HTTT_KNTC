using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.Complain;

public class KQGQHoSo : FullAuditedEntity<Guid>
{
    public KQGQHoSo()
    {

    }
    public KQGQHoSo(Guid id) : base(id)
    {

    }
    public Guid IdHoSo { get; set; }
    public HoSo HoSo { get; set; }
    public short LanGQ { get; set; }
    public DateTime NgayTraKQ { get; set; }
    public string ThamQuyen { get; set; }
    public string SoQD { get; set; }
    public string GhiChu { get; set; }
    public LoaiKetQua KetQua { get; set; }
}
    
