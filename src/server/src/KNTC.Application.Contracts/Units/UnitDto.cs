using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.Units;

public class UnitDto : EntityDto<Guid>
{
    public string UnitCode { get; set; }
    public string UnitName { get; set; }
    public string ShortName { get; set; }
    public int UnitTypeId { get; set; }
    public int ParentId { get; set; }
    public string Pescription { get; set; }
    public int OrderIndex { get; set; }
    public int Status { get; set; }

}
