using KNTC.CategoryUnitTypes;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace KNTC.LandTypes;
public class LandTypeLookupCache
{
    public List<LandTypeLookupDto> Items { get; set; }
}
public class LandTypeLookupDto : EntityDto<int>
{
    public string LandTypeName { get; set; }
}
