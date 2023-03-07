using Volo.Abp.Application.Dtos;

namespace KNTC.UnitTypes;

public class UnitTypeDto : EntityDto<int>
{
    public string UnitTypeCode { get; set; }
    public string UnitTypeName { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public int Status { get; set; }

}
