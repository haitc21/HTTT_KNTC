﻿using Volo.Abp.Caching;

namespace KNTC.SysConfigs;

[CacheName("SysConfig")]
public class SysConfigCacheItem
{
    public string Name { get; set; }
    public string Value { get; set; }
}