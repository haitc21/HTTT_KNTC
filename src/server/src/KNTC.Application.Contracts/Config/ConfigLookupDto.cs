using Volo.Abp.Application.Dtos;

namespace KNTC.Configs;

public class ConfigLookupDto : EntityDto<int>
{
    public string ConfigName { get; set; }
}
