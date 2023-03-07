using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.DocumentTypes;

public class DocumentTypeDto : FullAuditedEntityDto<Guid>
{
    public string DocumentTypeCode { get; set; }
    public string DocumentTypeName { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public int Status { get; set; }

}
