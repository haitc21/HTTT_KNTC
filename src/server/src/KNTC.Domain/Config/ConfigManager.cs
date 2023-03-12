using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.Configs;

public class ConfigManager : DomainService
{
    private readonly IRepository<Config, int> _configRepo;
    public ConfigManager(IRepository<Config, int> configRepo)
    {
        _configRepo = configRepo;
    }
    public async Task<Config> CreateAsync([NotNull] string code,
                                                [NotNull] string name,
                                                string ToaDo,
                                                string Tel,
                                                string Address,
                                                string description)
    {

        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        await CheckCode(code);
        await CheckName(name);
        return new Config(code, name)
        {
            ToaDo = ToaDo,
            Tel = Tel,
            Address = Address,
            Description = description,
            Status = Status.Active
        };
    }
    public async Task UpdateAsync([NotNull] Config config,
                                                 [NotNull] string code,
                                                [NotNull] string name,
                                                string ToaDo,
                                                string Tel,
                                                string Address,
                                                string description)
    {
        Check.NotNull(config, nameof(config));
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        if (config.OrganizationCode != code)
        {
            await ChangeCode(config, code);
        }
        if (config.OrganizationName != name)
        {
            await ChangeName(config, name);
        }
        config.ToaDo = ToaDo;
        config.Tel = Tel;
        config.Address = Address;
        config.Description = description;
    }
    private async Task ChangeName(Config unitType, string name)
    {
        var existedName = await _configRepo.FindAsync(x => x.OrganizationName == name, false);
        if (existedName != null && existedName.Id != unitType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
        unitType.ChangeName(name);
    }
    private async Task CheckName(string name)
    {
        var existedName = await _configRepo.FindAsync(x => x.OrganizationName == name, false);
        if (existedName != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
    }
    private async Task ChangeCode(Config unitType, string code)
    {
        var existedCode = await _configRepo.FindAsync(x => x.OrganizationCode == code, false);
        if (existedCode != null && existedCode.Id != unitType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
        unitType.ChangeCode(code);
    }
    private async Task CheckCode(string code)
    {
        var existedCode = await _configRepo.FindAsync(x => x.OrganizationCode == code, false);
        if (existedCode != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
    }
}
