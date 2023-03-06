using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.CommonList.Dtos;

public class UnitTypeDto : AuditedEntityDto<Guid>
{
    public Guid id { get; set; }
    public string unitTypeCode { get; set; }
    public string unitTypeName { get; set; }
    public string description { get; set; }
    public int orderIndex { get; set; }
    public int status { get; set; }

}
