using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.Units;

public class CreateAndUpdateUnitDto : EntityDto<int>, IHasConcurrencyStamp
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxCodeLength)]
    public string UnitCode { get; set; }

    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNameLength)]
    public string UnitName { get; set; }

    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNameLength)]
    public string ShortName { get; set; }

    public int UnitTypeId { get; set; }
    public int? ParentId { get; set; }

    [MaxLength(KNTCValidatorConsts.MaxDescriptionLength)]
    public string? Description { get; set; }

    public int? OrderIndex { get; set; }
    public Status Status { get; set; }
    public string? ConcurrencyStamp { get; set; }
}