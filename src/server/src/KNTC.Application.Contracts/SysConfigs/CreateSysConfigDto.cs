using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.SysConfigs;

public class CreateSysConfigDto : EntityDto<int>, IHasConcurrencyStamp
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNameLength)]
    [RegularExpression(@"^[a-zA-Z_][a-zA-Z0-9_]*$", ErrorMessage = "Tên không hợp lệ")]
    public string Name { get; set; }

    [Required]
    public string Value { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxDescriptionLength)]
    public string Description { get; set; }
    public string ConcurrencyStamp { get; set; }
}