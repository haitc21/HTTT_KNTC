using System;
using System.Collections.Generic;

namespace KNTC.Complains;

public class GetComplainListDto : BaseListFilterDto
{
    public int? maTinhTP { get; set; }
    public int? maQuanHuyen { get; set; }
    public int? maXaPhuongTT { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public LinhVuc? LinhVuc { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public int? GiaiDoan { get; set; }
    public bool? CongKhai { get; set; }
}
