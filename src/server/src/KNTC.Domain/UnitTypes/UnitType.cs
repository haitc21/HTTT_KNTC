using KNTC.Units;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.UnitTypes;

public class UnitType : FullAuditedEntity<int>
{
    public string UnitTypeCode { get; set; }
    public string UnitTypeName { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public Status Status { get; set; }
    public virtual List<Unit> Units { get; set; }

}
