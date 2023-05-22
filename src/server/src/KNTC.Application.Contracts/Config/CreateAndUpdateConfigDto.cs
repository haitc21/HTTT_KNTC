using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.Configs;

public class CreateAndUpdateConfigDto : EntityDto<int>, IHasConcurrencyStamp
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxCodeLength)]
    public string OrganizationCode { get; set; }

    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNameLength)]
    public string OrganizationName { get; set; }

    [MaxLength(KNTCValidatorConsts.MaxDescriptionLength)]
    public string ToaDo { get; set; }

    public string Tel { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public string ConcurrencyStamp { get; set; }
}