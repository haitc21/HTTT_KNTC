using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.LandTypes;

public interface ILandTypeAppService
    : ICrudAppService<LandTypeDto,
        int,
        GetLandTypesListDto,
        CreateAndUpdateLandTypeDto>
{
    Task<ListResultDto<LandTypeLookupDto>> GetLookupAsync();

    Task DeleteMultipleAsync(IEnumerable<int> ids);
}