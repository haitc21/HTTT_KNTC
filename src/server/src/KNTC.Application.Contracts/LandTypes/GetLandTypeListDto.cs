using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC.LandTypes;

public class GetLandTypeListDto : BaseListFilterDto
{
    public Status? Status { get; set; }
}
