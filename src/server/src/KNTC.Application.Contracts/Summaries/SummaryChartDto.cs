using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC.Summaries;

public class SummaryChartDto
{
    public int LandComplain { get; set; }
    public int EnviromentComplain { get; set; }
    public int WaterComplain { get; set; }
    public int MineralComplain { get; set; }

    public int LandDenounce { get; set; }
    public int EnviromentDenounce { get; set; }
    public int WaterDenounce { get; set; }
    public int MineralDenounce { get; set; }
}
