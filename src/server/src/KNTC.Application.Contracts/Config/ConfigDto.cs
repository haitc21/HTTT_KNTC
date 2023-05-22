using Volo.Abp.Application.Dtos;

namespace KNTC.Configs;

public class ConfigDto : FullAuditedEntityDto<int>
{
    public string OrganizationCode { get; set; }
    public string OrganizationName { get; set; }
    public string ToaDo { get; set; }
    public string Tel { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public string ConcurrencyStamp { get; set; }
}