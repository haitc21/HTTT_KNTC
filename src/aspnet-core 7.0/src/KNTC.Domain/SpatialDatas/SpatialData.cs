using NetTopologySuite.Geometries;
using System;
using Volo.Abp.Domain.Entities;

namespace KNTC.SpatialDatas;

public class SpatialData : Entity<Guid>
{
    public SpatialData()
    {
    }

    public SpatialData(Guid Id) : base(Id)
    {
    }

    public LoaiVuViec LoaiVuViec { get; set; }
    public NetTopologySuite.Geometries.Point? DuLieuToaDo { get; set; }
    public NetTopologySuite.Geometries.Geometry? DuLieuHinhHoc { get; set; }
}