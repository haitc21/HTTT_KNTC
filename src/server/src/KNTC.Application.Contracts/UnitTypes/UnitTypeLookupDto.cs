using Volo.Abp.Application.Dtos;

namespace KNTC.UnitTypes;

public class UnitTypeLookupDto : EntityDto<int>
{
    public string UnitTypeName { get; set; }
}
