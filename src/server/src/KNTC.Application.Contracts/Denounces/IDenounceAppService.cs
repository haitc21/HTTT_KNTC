using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KNTC.Denounces;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.Denounces;

public interface IDenounceAppService :
        ICrudAppService<
            DenounceDto,
            Guid,
            GetDenounceListDto,
            CreateUnitTypeDto,
            UpdateUnitTypeDto>
{
    Task DeleteMultipleAsync(IEnumerable<Guid> ids);
    Task<byte[]> DowloadAsync(string idTepDinhKem);
}
