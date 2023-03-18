using Microsoft.SqlServer.Types;
using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.SpatialDatas;

public class SpatialDataDto : EntityDto<int>
{
    public Double ObjectId { get; }
    public string? TenToChuc { get; }
    public Single? Quyen { get; }
    public string SoToBD { get; }
    public SqlGeometry? Geometry { get; set; }
    public string GeoJson { get; }
}
