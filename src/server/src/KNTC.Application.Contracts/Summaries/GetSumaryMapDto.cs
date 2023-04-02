using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Summaries;

public class GetSumaryMapDto
{
    public string Keyword { get; set; }
    public bool LandComplain { get; set; }
    public bool EnviromentComplain { get; set; }
    public bool WaterComplain { get; set; }
    public bool MineralComplain { get; set; }

    public bool LandDenounce { get; set; }
    public bool EnviromentDenounce { get; set; }
    public bool WaterDenounce { get; set; }
    public bool MineralDenounce { get; set; }

    public int? maTinhTP { get; set; }
    public int? maQuanHuyen { get; set; }
    public int? maXaPhuongTT { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public bool? CongKhai { get; set; }
}
