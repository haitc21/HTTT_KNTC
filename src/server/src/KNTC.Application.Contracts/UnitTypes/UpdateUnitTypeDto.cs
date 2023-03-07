using KNTC.Complains;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace KNTC.UnitTypes;

public class UpdateUnitTypeDto : EntityDto<Guid>
{
    [Required]
    [MaxLength(UnitTypeConsts.MaxUnitTypeCodeLength)]
    public string UnitTypeCode { get; set; }
    [Required]
    [MaxLength(UnitTypeConsts.MaxUnitTypeNameLength)]
    public string UnitTypeName { get; set; }
    [MaxLength(UnitTypeConsts.MaxDescriptionLength)]
    public string Description { get; set; }

    public string OrderIndex { get; set; }

    public int Status { get; set; }

    public string ConcurrencyStamp { get; set; }
}
