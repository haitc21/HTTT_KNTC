using System.ComponentModel.DataAnnotations;
using KNTC.Complains;

namespace KNTC.UnitTypes;

public class CreateUnitTypeDto
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
}
