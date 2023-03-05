using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC.Complains;

public class GetComplainListDto : BaseListFilterDto
{
    public LoaiVuViec? LoaiVuViec { get; set; }
    public LinhVuc? LinhVuc { get; set; }
    public LoaiKetQua? KetQua { get; set; }
}
