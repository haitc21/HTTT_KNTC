using Microsoft.SqlServer.Types;
using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.SpatialDatas;

public class SpatialDataDto : FullAuditedEntityDto<int>
{
    //public SqlGeometry geometry { get; set; }
    public string geoJson { get; }
}
