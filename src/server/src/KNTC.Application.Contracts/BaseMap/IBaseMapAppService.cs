using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.BaseMaps;

public interface IBaseMapAppService
    : ICrudAppService<BaseMapDto,
        int,
        GetBaseMapListDto,
        CreateAndUpdateBaseMapDto>
{
    Task<ListResultDto<BaseMapLookupDto>> GetLookupAsync();

    Task DeleteMultipleAsync(IEnumerable<int> ids);
}