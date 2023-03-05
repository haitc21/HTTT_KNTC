using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.CommonList.Dtos;

public class TownDto : AuditedEntityDto<Guid>
{
    public Guid IdXaThitran { get; set; }
    public Guid IdQuanHuyen { get; set; }
    public string MaXaThitran { get; set; }
    public string TenXaThitran { get; set; }
    public string GhiChu { get; set; }
    public int Status { get; set; }
    
}
