using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC.Complains;

public class GetComplainListDto : BaseListFilterDto
{
    public int? maTinhTP { get; set; }
    public int? maQuanHuyen { get; set; }
    public int? maXaPhuongTT { get; set; }
    public DateTime? NgayTiepNhan { get; set; }
    public LoaiVuViec? LoaiVuViec { get; set; }
    public LinhVuc? LinhVuc { get; set; }
    public LoaiKetQua? KetQua { get; set; }
}
