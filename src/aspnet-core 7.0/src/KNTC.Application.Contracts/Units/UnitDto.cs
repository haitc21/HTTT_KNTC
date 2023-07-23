using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.Units;

public class UnitDto : FullAuditedEntityDto<int>, IHasConcurrencyStamp
{
    public string UnitCode { get; set; }
    public string UnitName { get; set; }
    public string ShortName { get; set; }
    public int UnitTypeId { get; set; }
    public int? ParentId { get; set; }
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public Status Status { get; set; }
    public string? ConcurrencyStamp { get; set; }
}