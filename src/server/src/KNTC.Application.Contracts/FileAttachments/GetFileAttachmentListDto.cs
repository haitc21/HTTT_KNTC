using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC.FileAttachments;

public class GetFileAttachmentListDto : BaseListFilterDto
{
    public Guid? ComplainId { get; set; }
    public Guid? DenounceId { get; set; }
    public int? GiaiDoan { get; set; }
    public int? HinhThuc { get; set; }
}
