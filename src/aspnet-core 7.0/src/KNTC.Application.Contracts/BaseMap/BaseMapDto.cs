using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.BaseMaps;

public class BaseMapDto : FullAuditedEntityDto<int>, IHasConcurrencyStamp
{
    public string BaseMapCode { get; set; }
    public string BaseMapName { get; set; }
    public string? Description { get; set; }
    public int? OrderIndex { get; set; }
    public Status Status { get; set; }
    public string? ConcurrencyStamp { get; set; }
}