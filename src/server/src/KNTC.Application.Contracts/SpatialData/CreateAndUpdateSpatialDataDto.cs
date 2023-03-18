using Microsoft.SqlServer.Types;
using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace KNTC.SpatialDatas;

public class CreateAndUpdateSpatialDataDto : EntityDto<int>
{
    public Double ObjectId { get; }
    [MaxLength(KNTCValidatorConsts.MaxTenToChucLength)]
    public string? TenToChuc { get; }
    public Single? Quyen { get; }
    [MaxLength(KNTCValidatorConsts.MaxToBanDoLength)]
    public string SoToBD { get; }
    public SqlGeometry? Geometry { get; set; }
    public string GeoJson { get; }
}
