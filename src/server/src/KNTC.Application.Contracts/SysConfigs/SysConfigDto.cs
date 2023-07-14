using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.SysConfigs;

public class SysConfigDto : EntityDto<int>, IHasConcurrencyStamp
{
    public string Name { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }
    public string ConcurrencyStamp { get; set; }
}