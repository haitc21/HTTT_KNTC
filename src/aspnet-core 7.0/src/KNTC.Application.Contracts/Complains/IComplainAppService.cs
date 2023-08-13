using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace KNTC.Complains;

public interface IComplainAppService :
        ICrudAppService<
            ComplainDto,
            Guid,
            GetComplainListDto,
            CreateComplainDto,
            UpdateComplainDto>
{
    Task DeleteMultipleAsync(IEnumerable<Guid> ids);

    Task<byte[]> GetExcelAsync(GetComplainListDto input);
}