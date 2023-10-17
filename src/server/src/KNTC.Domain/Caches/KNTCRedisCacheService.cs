using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KNTC.Caches;

public class KNTCRedisCacheService : IKNTCRedisCacheService
{
    private IConfiguration configuration;

    public KNTCRedisCacheService(IConfiguration configuration = null)
    {
        this.configuration = configuration;
    }

    public async Task DeleteContainAsync(string keyword)
    {
        string host = configuration["Redis:Host"];
        string port = configuration["Redis:Port"];
        if (host.IsNullOrEmpty() || port.IsNullOrEmpty()) return;
        string password = configuration["Redis:Password"];
        string connectionStr = $"{host}:{port}";
        if (!password.IsNullOrEmpty()) connectionStr += $",password={password}";
        using (ConnectionMultiplexer con = ConnectionMultiplexer.Connect(connectionStr))
        {
            var db = con.GetDatabase();
            var sv = con.GetServer($"{host}:{port}");
            var keys = sv.Keys(pattern: $"*{keyword}*").ToArray();
            await db.KeyDeleteAsync(keys);
        }
    }
}