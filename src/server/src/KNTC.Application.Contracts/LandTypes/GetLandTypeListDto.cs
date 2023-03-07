using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC.LandTypes;

internal class GetLandTypeListDto : BaseListFilterDto
{
    public Status Status { get; set; }
}
