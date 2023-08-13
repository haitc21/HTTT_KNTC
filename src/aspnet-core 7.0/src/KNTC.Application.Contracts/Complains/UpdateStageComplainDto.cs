using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.Complains;

public class UpdateStageComplainDto : EntityDto<Guid>, IHasConcurrencyStamp
{
    public TrangThai TrangThai { get; set; }
    public string? GhiChu { get; set; }
    public string ConcurrencyStamp { get; set; }
}
