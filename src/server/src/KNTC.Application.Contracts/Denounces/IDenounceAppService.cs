using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace KNTC.Denounces;

public interface IDenounceAppService :
        ICrudAppService<
            DenounceDto,
            Guid,
            GetDenounceListDto,
            CreateDenounceDto,
            UpdateDenounceDto>
{
    Task DeleteMultipleAsync(IEnumerable<Guid> ids);
    Task<byte[]> DowloadAsync(string idTepDinhKem);
}
