using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.CommonList.Dtos;

public class DistrictDto : AuditedEntityDto<Guid>
{
    public Guid IdQuanHuyen { get; set; }
    public Guid IdTinhThanhPho { get; set; }
    public string MaQuanHuyen { get; set; }
    public string TenQuanHuyen { get; set; }
    public string GhiChu { get; set; }
    public int Status { get; set; }
    
}
