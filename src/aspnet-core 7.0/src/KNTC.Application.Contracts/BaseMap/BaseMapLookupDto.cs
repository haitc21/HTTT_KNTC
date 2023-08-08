using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace KNTC.BaseMaps;

[CacheName("BaseMapLookup")]
public class BaseMapLookupCache
{
    public List<BaseMapLookupDto> Items { get; set; }
}

public class BaseMapLookupDto : EntityDto<int>
{
    public string BaseMapName { get; set; }
}