using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.BaseMaps;

public class BaseMapManager : DomainService
{
    private readonly IRepository<BaseMap, int> _BaseMapRepo;

    public BaseMapManager(IRepository<BaseMap, int> BaseMapRepo)
    {
        _BaseMapRepo = BaseMapRepo;
    }

    public async Task<BaseMap> CreateAsync([NotNull] string code,
                                                [NotNull] string name,
                                                string? description,
                                                int? orderIndex,
                                                Status status)
    {
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        await CheckCode(code);
        await CheckName(name);
        return new BaseMap(code, name)
        {
            Description = description,
            OrderIndex = orderIndex,
            Status = status
        };
    }

    public async Task UpdateAsync([NotNull] BaseMap BaseMap,
                                  [NotNull] string code,
                                  [NotNull] string name,
                                  string? description,
                                  int? orderIndex,
                                  Status status)
    {
        Check.NotNull(BaseMap, nameof(BaseMap));
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        if (BaseMap.BaseMapCode != code)
        {
            await ChangeCode(BaseMap, code);
        }
        if (BaseMap.BaseMapName != name)
        {
            await ChangeName(BaseMap, name);
        }
        BaseMap.Description = description;
        BaseMap.OrderIndex = orderIndex;
        BaseMap.Status = status;
    }

    private async Task ChangeName(BaseMap BaseMap, string name)
    {
        var existedName = await _BaseMapRepo.FindAsync(x => x.BaseMapName == name, false);
        if (existedName != null && existedName.Id != BaseMap.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
        BaseMap.ChangeName(name);
    }

    private async Task CheckName(string name)
    {
        var existedName = await _BaseMapRepo.FindAsync(x => x.BaseMapName == name, false);
        if (existedName != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
    }

    private async Task ChangeCode(BaseMap BaseMap, string code)
    {
        var existedCode = await _BaseMapRepo.FindAsync(x => x.BaseMapCode == code, false);
        if (existedCode != null && existedCode.Id != BaseMap.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
        BaseMap.ChangeCode(code);
    }

    private async Task CheckCode(string code)
    {
        var existedCode = await _BaseMapRepo.FindAsync(x => x.BaseMapCode == code, false);
        if (existedCode != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
    }
}