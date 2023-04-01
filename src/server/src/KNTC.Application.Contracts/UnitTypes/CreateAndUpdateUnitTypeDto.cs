using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.CategoryUnitTypes;

public class CreateAndUpdateUnitTypeDto : EntityDto<int>, IHasConcurrencyStamp
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxCodeLength)]
    public string UnitTypeCode { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNameLength)]
    public string UnitTypeName { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxDescriptionLength)]
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public Status Status { get; set; }
    public string ConcurrencyStamp { get; set; }

}
