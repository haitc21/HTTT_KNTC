using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.SysConfigs;

public class UpdateSysConfigDto : EntityDto<int>, IHasConcurrencyStamp
{
    [Required]
    public string Value { get; set; }

    [MaxLength(KNTCValidatorConsts.MaxDescriptionLength)]
    public string? Description { get; set; }

    public string? ConcurrencyStamp { get; set; }
}