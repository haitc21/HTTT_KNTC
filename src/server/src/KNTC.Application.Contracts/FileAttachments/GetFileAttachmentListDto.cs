using System;

namespace KNTC.FileAttachments;

public class GetFileAttachmentListDto : BaseListFilterDto
{
    public Guid? ComplainId { get; set; }
    public Guid? DenounceId { get; set; }
    public int? GiaiDoan { get; set; }
    public int? HinhThuc { get; set; }
    public bool? CongKhai { get; set; }
}