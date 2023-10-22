namespace KNTC.Units;

public class UnitLookupByIdsKey
{
    public UnitLookupByIdsKey(int[]? unitIds)
    {
        UnitIds = unitIds;
    }

    public int[]? UnitIds { get; set; }

    public override string ToString()
    {
        if (UnitIds == null || UnitIds.Length == 0)
        {
            return string.Empty;
        }

        string key = string.Join("_", UnitIds);
        int hash = key.GetHashCode();
        return $"{nameof(UnitCacheKey)}_ByIds_{hash}";
    }
}