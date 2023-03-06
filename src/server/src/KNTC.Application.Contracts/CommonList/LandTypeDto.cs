using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.CommonList.Dtos;

public class LandTypeDto : AuditedEntityDto<Guid>
{
    public Guid id { get; set; }
    public string landTypeCode { get; set; }
    public string landTypeName { get; set; }
    public string description { get; set; }
    public int orderIndex { get; set; }
    public int status { get; set; }

}
