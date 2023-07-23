namespace KNTC.Units;

public class UnitCacheKey
{
    public UnitCacheKey(int unitTypeId, int? parentId = null)
    {
        UnitTypeId = unitTypeId;
        ParentId = parentId;
    }

    public int UnitTypeId { get; set; }
    public int? ParentId { get; set; }

    public override string ToString()
    {
        return $"{UnitTypeId}_{ParentId}";
    }
}