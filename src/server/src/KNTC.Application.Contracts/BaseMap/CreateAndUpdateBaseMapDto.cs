﻿using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.BaseMaps;

public class CreateAndUpdateBaseMapDto : EntityDto<int>, IHasConcurrencyStamp
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxCodeLength)]
    public string BaseMapCode { get; set; }

    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNameLength)]
    public string BaseMapName { get; set; }

    [MaxLength(KNTCValidatorConsts.MaxDescriptionLength)]
    public string? Description { get; set; }

    public int? OrderIndex { get; set; }
    public Status Status { get; set; }
    public string? ConcurrencyStamp { get; set; }
}