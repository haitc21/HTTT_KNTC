using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.LandTypes;

public class CreateAndUpdateLandTypeDto : EntityDto<int>, IHasConcurrencyStamp
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxCodeLength)]
    public string LandTypeCode { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNameLength)]
    public string LandTypeName { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxDescriptionLength)]
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public int Status { get; set; }
    public string ConcurrencyStamp { get; set; }

}
