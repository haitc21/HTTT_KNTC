using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.UnitTypes;

public class GetUnitListDto : BaseListFilterDto
{
    public Status Status { get; set; }
}
