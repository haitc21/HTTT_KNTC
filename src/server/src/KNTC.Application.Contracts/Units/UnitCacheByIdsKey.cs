namespace KNTC.Units;

public class UnitCacheByIdsKey
{
    public UnitCacheByIdsKey(int[]? _unitIds)
    {
        unitIds = _unitIds;
    }

    public int[]? unitIds { get; set; }

    public override string ToString()
    {
        return unitIds.ToString();
    }
}