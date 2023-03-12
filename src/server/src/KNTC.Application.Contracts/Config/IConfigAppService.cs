using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.Configs;

public interface IConfigAppService :
    ICrudAppService<ConfigDto,
        int,
        GetConfigListDto,
        CreateAndUpdateConfigDto>
{
    Task<ListResultDto<ConfigLookupDto>> GetLookupAsync();
    Task DeleteMultipleAsync(IEnumerable<int> ids);
}
