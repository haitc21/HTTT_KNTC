using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace KNTC.RedisCache;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;
    private IConfiguration configuration;

    public RedisCacheService(IDistributedCache cache, IConfiguration configuration = null)
    {
        _cache = cache;
        this.configuration = configuration;
    }

    public async Task DeleteCacheKeysSContainAsync(string keyword)
    {
        string host = configuration["Redis:Configuration"];
        string port = "6379";
        var connection = ConnectionMultiplexer.Connect(host);
        var keys = connection.GetServer($"{host}:{port}").Keys(pattern: $"*{keyword}*");

        foreach (var key in keys)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
