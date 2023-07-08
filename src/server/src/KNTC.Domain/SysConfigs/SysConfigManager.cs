using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.SysConfigs;

public class SysConfigManager : DomainService
{
    private readonly IRepository<SysConfig, int> _configRepo;

    public SysConfigManager(IRepository<SysConfig, int> configRepo)
    {
        _configRepo = configRepo;
    }

    public async Task<SysConfig> CreateAsync([NotNull] string name, [NotNull] string value, string description)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(value, nameof(value));
        await CheckName(name);
        return new SysConfig(name, value)
        {
            Description = description
        };
    }

    public async Task UpdateAsync([NotNull] SysConfig sysConfig,
                                                [NotNull] string value,
                                                string description)
    {
        Check.NotNull(sysConfig, nameof(sysConfig));
        Check.NotNullOrWhiteSpace(value, nameof(value));
        sysConfig.Value = value;
        sysConfig.Description = description;
    }

    private async Task ChangeName(SysConfig sysConfig, string name)
    {
        var existedName = await _configRepo.FindAsync(x => x.Value == name, false);
        if (existedName != null && existedName.Id != sysConfig.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
        sysConfig.ChangeName(name);
    }

    private async Task CheckName(string name)
    {
        var existedName = await _configRepo.FindAsync(x => x.Value == name, false);
        if (existedName != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
    }
}