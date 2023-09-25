namespace KNTC.Units;

public class UnitCacheKey
{
    public UnitCacheKey(int[]? unitIds)
    {
        UnitIds = unitIds;
    }

    public UnitCacheKey(int unitTypeId, int? parentId = null)
    {
        UnitTypeId = unitTypeId;
        ParentId = parentId;
    }

    public UnitCacheKey(int unitTypeId, int[]? parentIds)
    {
        UnitTypeId = unitTypeId;
        ParentIds = parentIds;
    }

    public int UnitTypeId { get; set; }
    public int? ParentId { get; set; }
    public int[]? ParentIds { get; set; }
    public int[]? UnitIds { get; }

    public override string ToString()
    {
        return $"{UnitTypeId}_{ParentId}";
    }
}