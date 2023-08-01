using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace KNTC.SysConfigs;

public interface ISysConfigAppService :
    ICrudAppService<SysConfigDto,
        int,
        GetSysConfigListDto,
        CreateSysConfigDto,
        UpdateSysConfigDto>
{
    Task<SysConfigCacheItem> GetByNameAsync(string name);

    Task DeleteMultipleAsync(IEnumerable<int> ids);

    Task<AllSysConfigCacheItem> GetAllConfigsAsync();
}