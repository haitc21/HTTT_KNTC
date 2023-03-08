using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.UnitTypes;

public class GetUnitTypeListDto : BaseListFilterDto
{
    public Status? Status { get; set; }
}
