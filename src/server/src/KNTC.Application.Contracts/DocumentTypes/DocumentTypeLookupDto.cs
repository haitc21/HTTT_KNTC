using KNTC.LandTypes;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace KNTC.DocumentTypes;
public class DocumentTypeLookupCache
{
    public List<DocumentTypeLookupDto> Items { get; set; }
}
public class DocumentTypeLookupDto : EntityDto<int>
{
    public string DocumentTypeName { get; set; }
}
