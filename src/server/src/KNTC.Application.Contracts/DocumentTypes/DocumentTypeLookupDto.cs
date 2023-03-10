using Volo.Abp.Application.Dtos;

namespace KNTC.DocumentTypes;

public class DocumentTypeLookupDto : EntityDto<int>
{
    public string DocumentTypeName { get; set; }
}
