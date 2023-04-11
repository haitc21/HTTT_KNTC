using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace KNTC.RedisCache;

public interface IRedisCacheService : IDomainService
{
    Task DeleteCacheKeysSContainAsync(string prefix);
}
