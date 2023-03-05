using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC.Denounces;

public class GetDenounceListDto : BaseListFilterDto
{
    public LoaiVuViec? LoaiVuViec { get; set; }
    public LinhVuc? LinhVuc { get; set; }
    public LoaiKetQua? KetQua { get; set; }
}
