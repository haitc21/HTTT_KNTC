using Microsoft.SqlServer.Types;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace KNTC.SpatialDatas;

public class CreateAndUpdateSpatialDataDto : EntityDto<int>
{
    [Required]
    public SqlGeometry geometry { get; set; }
    public string geoJson { get; }
}
