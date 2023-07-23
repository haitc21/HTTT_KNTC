using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace KNTC.RedisCache;

public interface IRedisCacheService : IDomainService
{
    Task DeleteCacheKeysSContainAsync(string prefix);
}