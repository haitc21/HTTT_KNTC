using System.ComponentModel.DataAnnotations;
namespace KNTC.Units;

public class CreateUnitDto
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
    public string Pescription { get; set; }
    public int OrderIndex { get; set; }
    public int Status { get; set; }
}
