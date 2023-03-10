using Volo.Abp.Application.Dtos;

namespace KNTC.LandTypes;

public class LandTypeLookupDto : EntityDto<int>
{
    public string LandTypeName { get; set; }
}
