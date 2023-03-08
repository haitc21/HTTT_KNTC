using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC.DocumentTypes;

public class GetDocumentTypesListDto : BaseListFilterDto
{
    public Status? Status { get; set; }
}
