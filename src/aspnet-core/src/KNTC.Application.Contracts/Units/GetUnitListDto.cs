namespace KNTC.Units;

public class GetUnitListDto : BaseListFilterDto
{
    public Status? Status { get; set; }
    public int? UnitTypeId { get; set; }
    public int? ParentId { get; set; }
}