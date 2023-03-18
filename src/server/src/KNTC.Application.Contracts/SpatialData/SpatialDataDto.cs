using Microsoft.SqlServer.Types;
using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.SpatialDatas;

public class SpatialDataDto : EntityDto<int>
{
    //public int Id { get; }
    public Double OBJECTID { get; }
    public string TenToChuc { get; }
    public Single Quyen { get; }
    public string So_to_BD { get; }
    public SqlGeometry geometry { get; set; }
    public string geoJson { get; }
}
