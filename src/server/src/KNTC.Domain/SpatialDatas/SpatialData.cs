using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.SpatialDatas;

public class SpatialData : Entity<int>
{
    public SpatialData()
    {

    }
    public SpatialData(int id) : base(id)
    {

    }
    public Double ObjectId { get; set; }
    public string TenToChuc { get; set; }
    public Single? Quyen { get; set; }
    public string SoToBD { get; set; }
    public Geometry? Geometry { get; set; }
    public string GeoJson { get; set; }
}
