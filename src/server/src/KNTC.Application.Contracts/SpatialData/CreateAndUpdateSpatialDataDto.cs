using Microsoft.SqlServer.Types;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace KNTC.SpatialDatas;

public class CreateAndUpdateSpatialDataDto : EntityDto<int>
{
    //[Required]
    //public Geometry geometry { get; set; }
    public string geoJson { get; }
}
