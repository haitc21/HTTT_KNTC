using Microsoft.SqlServer.Types;
using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.SpatialDatas;

public class SpatialDataDto : EntityDto<int>
{
    public Double ObjectId { get; set; }
    public string? TenToChuc { get; set; }
    public Single? Quyen { get; set; }
    public string? SoToBD { get; set; }
    //public SqlGeometry Geometry { get;  set; }
    public string GeoJson { get; set; }
}
