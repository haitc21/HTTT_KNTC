using Microsoft.SqlServer.Types;
using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.SpatialDatas;

public class SpatialData : FullAuditedAggregateRoot<int>
{
    public SpatialData()
    {

    }
    public SpatialData(int ID) : base(ID)
    {

    }
    public SpatialData(SqlGeometry geometry)
    {
        ChangeGeometry(geometry);
    }
    public SpatialData(int ID, SqlGeometry geometry) : base(ID)
    {
        ChangeGeometry(geometry);
    }
    public SqlGeometry geometry { get; set; }
    public string geoJson { get; set; }
    private void SetGeometry([NotNull] SqlGeometry geometry)
    {
        geometry = Check.NotNull(
            geometry,
            nameof(geometry)
        );
    }

    internal SpatialData ChangeGeometry([NotNull] SqlGeometry geometry)
    {
        SetGeometry(geometry);
        return this;
    }
}
