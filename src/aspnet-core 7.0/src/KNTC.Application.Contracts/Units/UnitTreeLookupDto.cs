using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Caching;

namespace KNTC.Units;

[CacheName("UnitTreeLookup")]
public class UnitTreeLookupCache
{
    public List<UnitTreeLookupDto> Items { get; set; }
}
public class UnitTreeLookupDto
{
    public int Id { get; set; }
    public string UnitName { get; set; }
    public int UnitTypeId { get; set; }
    public List<UnitTreeLookupDto> Children { get; set; }
}
