using Volo.Abp.Application.Dtos;

namespace KNTC.CategoryUnitTypes;

public class UnitTypeLookupDto : EntityDto<int>
{
    public string UnitTypeName { get; set; }
}
