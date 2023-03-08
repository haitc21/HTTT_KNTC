using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.Units;

public interface IUnitAppService : 
    ICrudAppService<UnitDto,
        int,
        GetUnitListDto,
        CreateAndUpdateUnitDto>
{
    Task<ListResultDto<UnitLookupDto>> GetLookupAsync();
    Task DeleteMultipleAsync(IEnumerable<int> ids);
}
