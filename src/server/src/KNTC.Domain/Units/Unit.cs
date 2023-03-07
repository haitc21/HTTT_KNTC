using KNTC.UnitTypes;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.Units;

public class Unit : FullAuditedEntity<Guid>
{
    public string UnitCode { get; set; }
    public string UnitName { get; set; }
    public string ShortName { get; set; }
    public int UnitTypeId { get; set; }
    public int ParentId { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public int Status { get; set; }
    public UnitType UnitType { get; set; }

}
