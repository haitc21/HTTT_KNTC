using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace KNTC.Units;
public class UnitLookupCache
{
    public List<UnitLookupDto> Items { get; set; }
}
public class UnitLookupDto : EntityDto<int>
{
    public string UnitName { get; set; }
}
