﻿using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace KNTC.DocumentTypes;

public class DocumentTypeDto : FullAuditedEntityDto<int>
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxCodeLength)]
    public string DocumentTypeCode { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNameLength)]
    public string DocumentTypeName { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxDescriptionLength)]
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public Status Status { get; set; }
    public string ConcurrencyStamp { get; set; }

}
