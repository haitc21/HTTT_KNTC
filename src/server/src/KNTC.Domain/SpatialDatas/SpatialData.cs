//using Microsoft.SqlServer.Types;
//using NetTopologySuite.Geometries;
using System;
using Volo.Abp.Domain.Entities;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.SpatialDatas;

public class SpatialData : Entity<int>
{
    public SpatialData()
    {

    }
    public SpatialData(int Id) : base(Id)
    {

    }
    public virtual Double ObjectId { get; set; }
    public virtual string? TenToChuc { get; set; }
    public virtual Single? Quyen { get; set; }
    public virtual string? SoToBD { get; set; }
    //public SqlGeometry Geometry { get; set; }
    public virtual string GeoJson { get; set; }
}
