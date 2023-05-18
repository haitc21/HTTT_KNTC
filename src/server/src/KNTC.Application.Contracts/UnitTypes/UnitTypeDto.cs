using Volo.Abp.Application.Dtos;

namespace KNTC.CategoryUnitTypes;

public class UnitTypeDto : FullAuditedEntityDto<int>
{
    public string UnitTypeCode { get; set; }
    public string UnitTypeName { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public Status Status { get; set; }
    public string ConcurrencyStamp { get; set; }
}