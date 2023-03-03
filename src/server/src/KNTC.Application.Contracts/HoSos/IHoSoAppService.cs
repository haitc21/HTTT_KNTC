using System;
using System.Collections.Generic;
using System.Text;
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
}
