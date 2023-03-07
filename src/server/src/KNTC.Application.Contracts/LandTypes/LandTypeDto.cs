using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.LandTypes;

public class LandTypeDto : FullAuditedEntityDto<Guid>
{
    public string LandTypeCode { get; set; }
    public string LandTypeName { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public Status Status { get; set; }
    public string ConcurrencyStamp { get; set; }

}
