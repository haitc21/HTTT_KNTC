using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Summaries;

public class GetSummaryListDto : PagedAndSortedResultRequestDto
{
    public bool LandComplain { get; set; }
    public bool EnviromentComplain { get; set; }
    public bool WaterComplain { get; set; }
    public bool MineralComplain { get; set; }

    public bool LandDenounce { get; set; }
    public bool EnviromentDenounce { get; set; }
    public bool WaterDenounce { get; set; }
    public bool MineralDenounce { get; set; }
}
