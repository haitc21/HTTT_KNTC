using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.UnitTypes;

public class UnitListDto : AuditedEntityDto<Guid>
{
    public int Status { get; set; }
}
