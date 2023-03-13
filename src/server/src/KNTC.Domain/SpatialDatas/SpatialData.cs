using Microsoft.SqlServer.Types;
using NetTopologySuite.Geometries;
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
    public SpatialData(Geometry geometry)
    {
        ChangeGeometry(geometry);
    }
    public SpatialData(int ID, Geometry geometry) : base(ID)
    {
        ChangeGeometry(geometry);
    }
    public Geometry Geometry { get; set; }
    public string geoJson { get; set; }
    private void SetGeometry([NotNull] Geometry geometry)
    {
        geometry = Check.NotNull(
            geometry,
            nameof(geometry)
        );
    }

    internal SpatialData ChangeGeometry([NotNull] Geometry geometry)
    {
        SetGeometry(geometry);
        return this;
    }
}
