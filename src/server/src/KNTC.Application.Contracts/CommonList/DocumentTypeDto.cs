using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.CommonList.Dtos;

public class TownDto : AuditedEntityDto<Guid>
{
    public Guid id { get; set; }
    public string documentTypeCode { get; set; }
    public string documentTypeName { get; set; }
    public string description { get; set; }
    public int orderIndex { get; set; }
    public int status { get; set; }

}
