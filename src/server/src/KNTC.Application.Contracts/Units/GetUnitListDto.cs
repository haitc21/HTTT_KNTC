using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.Units;

public class GetUnitListDto : BaseListFilterDto
{
    public Status? Status { get; set; }
}
