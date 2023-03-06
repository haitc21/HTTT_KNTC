using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.CommonList.Dtos;

public class UnitDto : AuditedEntityDto<Guid>
{
    public Guid id { get; set; }
    public string unitCode { get; set; }
    public string unitName { get; set; }
    public string shortName { get; set; }
    public int unitTypeId { get; set; }
    public int parentId { get; set; }
    public string description { get; set; }
    public int orderIndex { get; set; }
    public int status { get; set; }
    
}
