using Microsoft.IdentityModel.Tokens;
using System.Text;

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
    public int[]? UnitIds { get; set; }

    public override string ToString()
    {
        string unitIds = UnitIds == null ? string.Empty : $"_{string.Join(",", UnitIds)}";
        string parentIds = ParentIds == null ? string.Empty : $"_{string.Join(",", ParentIds)}";
        StringBuilder result = new StringBuilder($"{UnitTypeId}_{ParentId}");
        if (!unitIds.IsNullOrEmpty())
            result.Append(unitIds);
        if (!parentIds.IsNullOrEmpty())
            result.Append(parentIds);
        return result.ToString();
    }
}