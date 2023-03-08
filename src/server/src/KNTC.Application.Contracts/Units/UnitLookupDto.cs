using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Units;

public class UnitLookupDto : EntityDto<int>
{
    public string UnitName { get; set; }
}
