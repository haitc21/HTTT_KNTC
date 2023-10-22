using System;
using System.Text;

namespace KNTC.Summaries;

public class GetSumaryMapDto
{
    public GetSumaryMapDto()
    {
    }

    public LoaiVuViec? loaiVuViec { get; set; }
    public LinhVuc? linhVuc { get; set; }

    public bool LandComplain { get; set; }
    public bool EnviromentComplain { get; set; }
    public bool WaterComplain { get; set; }
    public bool MineralComplain { get; set; }

    public bool LandDenounce { get; set; }
    public bool EnviromentDenounce { get; set; }
    public bool WaterDenounce { get; set; }
    public bool MineralDenounce { get; set; }

    public int? MaTinhTP { get; set; }
    public int? MaQuanHuyen { get; set; }
    public int? MaXaPhuongTT { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public bool? CongKhai { get; set; }
    public TrangThai? TrangThai { get; set; }
    public string Keyword { get; set; }
    public string NguoiNopDon { get; set; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder
            .Append(LandComplain)
            .Append("_")
            .Append(EnviromentComplain)
            .Append("_")
            .Append(WaterComplain)
            .Append("_")
            .Append(MineralComplain)
            .Append("_")
            .Append(LandDenounce)
            .Append("_")
            .Append(EnviromentDenounce)
            .Append("_")
            .Append(WaterDenounce)
            .Append("_")
            .Append(MineralDenounce)
            .Append("_")
            .Append(MaTinhTP.HasValue ? MaTinhTP.ToString() : string.Empty)
            .Append("_")
            .Append(MaQuanHuyen.HasValue ? MaQuanHuyen.ToString() : string.Empty)
            .Append("_")
            .Append(MaXaPhuongTT.HasValue ? MaXaPhuongTT.ToString() : string.Empty)
            .Append("_")
            .Append(FromDate.HasValue ? FromDate?.ToString("dd-MM-yyyy") : string.Empty)
            .Append("_")
            .Append(ToDate.HasValue ? ToDate?.ToString("dd-MM-yyyy") : string.Empty)
            .Append("_")
            .Append(KetQua.HasValue ? KetQua.ToString() : string.Empty)
            .Append("_")
            .Append(CongKhai.HasValue ? CongKhai.ToString() : string.Empty)
            .Append("_")
            .Append(Keyword)
            .Append("_")
            .Append(NguoiNopDon);
        string result = "Summary_Map_" + stringBuilder.GetHashCode();
        return result;
    }
}