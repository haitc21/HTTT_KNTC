using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.DocumentTypes;

public class DocumentTypeLookupDto : EntityDto<int>
{
    public string DocumentTypeName { get; set; }
}
