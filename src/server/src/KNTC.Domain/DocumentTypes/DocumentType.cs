using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.DocumentTypes;

public class DocumentType : FullAuditedEntity<int>
{
    public string DocumentTypeCode { get; set; }
    public string DocumentTypeName { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public Status Status { get; set; }
    public virtual List<FileAttachment> FileAttachments { get; set; }

}
