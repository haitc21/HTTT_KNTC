using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace KNTC.Units;

public class UpdateUnitDto : EntityDto<Guid>
{
    [Required]
    [MaxLength(UnitConsts.MaxUnitCodeLength)]
    public string UnitCode { get; set; }
    [Required]
    [MaxLength(UnitConsts.MaxUnitNameLength)]
    public string UnitName { get; set; }
    [MaxLength(UnitConsts.MaxShortNameLength)]
    public string ShortName { get; set; }
    public int UnitTypeId { get; set; }
    public int ParentId { get; set; }
    [MaxLength(UnitConsts.MaxDescriptionLength)]
    public string Description { get; set; }

    public string OrderIndex { get; set; }

    public int Status { get; set; }

    public string ConcurrencyStamp { get; set; }
}
