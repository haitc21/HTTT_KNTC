using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.CommonList.Dtos;

public class CityDto : AuditedEntityDto<Guid>
{
    public Guid IdTinhThanhpho { get; set; }
    public string MaTinhThanhpho { get; set; }
    public string TenTinhThanhpho { get; set; }
    public string GhiChu { get; set; }
    public int Status { get; set; }
    
}
