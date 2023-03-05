using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KNTC.Complains;
using Volo.Abp.Application.Dtos;
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
    Task<byte[]> DowloadAsync(string idTepDinhKem);
}
