using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace KNTC.Caches;

public interface IKNTCRedisCacheService : IDomainService
{
    Task DeleteContainAsync(string keyword);
}