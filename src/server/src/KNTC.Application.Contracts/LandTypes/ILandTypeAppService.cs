using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.LandTypes;

public interface ILandTypeAppService 
    : ICrudAppService<LandTypeDto,
        int,
        GetLandTypeListDto,
        CreateAndUpdateLandTypeDto>
{
    Task<ListResultDto<LandTypeLookupDto>> GetLookupAsync();
    Task DeleteMultipleAsync(IEnumerable<int> ids);
}
