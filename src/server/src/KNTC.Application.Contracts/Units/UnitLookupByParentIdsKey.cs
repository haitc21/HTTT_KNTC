namespace KNTC.Units;

public class UnitLookupByParentIdsKey
{
    public UnitLookupByParentIdsKey(int[]? parentIds)
    {
        ParentIds = parentIds;
    }

    public UnitLookupByParentIdsKey(int unitTypeId, int[]? parentIds)
    {
        UnitTypeId = unitTypeId;
        ParentIds = parentIds;
    }

    public int UnitTypeId { get; set; }
    public int[]? ParentIds { get; set; }

    public override string ToString()
    {
        if (ParentIds == null || ParentIds.Length == 0)
        {
            return string.Empty;
        }

        string key = string.Join("_", ParentIds);
        int hash = key.GetHashCode();
        return $"{nameof(UnitCacheKey)}_ByParentIds_{hash}";
    }
}