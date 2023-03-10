using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.UnitTypes;

public interface IUnitTypeAppService :
    ICrudAppService<UnitTypeDto,
        int,
        GetUnitTypeListDto,
        CreateAndUpdateUnitTypeDto>
{
    Task<ListResultDto<UnitTypeLookupDto>> GetLookupAsync();
    Task DeleteMultipleAsync(IEnumerable<int> ids);
}
