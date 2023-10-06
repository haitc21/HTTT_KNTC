using System;

namespace KNTC.FileAttachments;

public class GetFileAttachmentListDto : BaseListFilterDto
{
    public Guid IdHoSo { get; set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public int? GiaiDoan { get; set; }
    public int? HinhThuc { get; set; }
    public bool? CongKhai { get; set; }
}