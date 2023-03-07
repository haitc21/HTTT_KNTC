using KNTC.Complains;
using KNTC.Denounces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.LandTypes;

public class LandType : FullAuditedEntity<int>
{
    public string LandTypeCode { get; set; }
    public string LandTypeName { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public int Status { get; set; }
    public virtual List<Complain> Complains { get; set; }
    public virtual List<Denounce> Denounces { get; set; }

}
