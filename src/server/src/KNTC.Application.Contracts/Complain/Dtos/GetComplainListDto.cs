using System;
using System.Collections.Generic;
using System.Text;
using KNTC.Complain;

namespace KNTC.Complain.Dtos;

public class GetComplainListDto : BaseListFilterDto
{
    public LoaiVuViec? LoaiVuViec { get; set; }
    public LinhVuc? LinhVuc { get; set; }
    public LoaiKetQua? KetQua { get; set; }
}
