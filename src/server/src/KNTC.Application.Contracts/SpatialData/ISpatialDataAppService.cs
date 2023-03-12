using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.SpatialDatas;

public interface ISpatialDataAppService :
    ICrudAppService<SpatialDataDto,
        int,
        GetSpatialDataListDto,
        CreateAndUpdateSpatialDataDto>
{
    Task<ListResultDto<SpatialDataLookupDto>> GetLookupAsync();
    Task DeleteMultipleAsync(IEnumerable<int> ids);
}
