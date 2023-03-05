using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.HoSos;

public interface IHoSoAppService :
        ICrudAppService< 
            HoSoDto, 
            Guid, 
            GetHoSoListDto,
            CreateHoSoDto,
            UpdateHoSoDto>
{
    Task DeleteMultipleAsync(IEnumerable<Guid> ids);
    Task<byte[]> DowloadAsync(string idTepDinhKem);
}
