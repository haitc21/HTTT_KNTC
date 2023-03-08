using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.UnitTypes;

public class UnitTypeLookupDto : EntityDto<int>
{
    public string UnitTypeName { get; set; }
}
