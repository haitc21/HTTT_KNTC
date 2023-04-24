using System;
using Volo.Abp.Domain.Entities;

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
    //public Geometry Geometry { get; set; }
    public virtual string GeoJson { get; set; }
}
