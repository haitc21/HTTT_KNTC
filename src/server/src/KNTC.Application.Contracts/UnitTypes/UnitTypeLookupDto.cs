using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace KNTC.CategoryUnitTypes;

public class UnitTypeLookupCache
{
    public List<UnitTypeLookupDto> Items { get; set; }
}
public class UnitTypeLookupDto : EntityDto<int>
{
    public string UnitTypeName { get; set; }
}
