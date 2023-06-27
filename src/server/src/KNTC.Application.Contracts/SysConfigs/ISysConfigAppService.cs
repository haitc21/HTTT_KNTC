using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.SysConfigs;

public interface ISysConfigAppService :
    ICrudAppService<SysConfigDto,
        int,
        GetSysConfigListDto,
        CreateSysConfigDto,
        UpdateSysConfigDto>
{
    Task DeleteMultipleAsync(IEnumerable<int> ids);
}