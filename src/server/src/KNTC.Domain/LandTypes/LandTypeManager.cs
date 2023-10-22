using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.LandTypes;

public class LandTypeManager : DomainService
{
    private readonly IRepository<LandType, int> _LandTypeRepo;

    public LandTypeManager(IRepository<LandType, int> LandTypeRepo)
    {
        _LandTypeRepo = LandTypeRepo;
    }

    public async Task<LandType> CreateAsync([NotNull] string code,
                                                [NotNull] string name,
                                                string? description,
                                                int? orderIndex,
                                                Status status)
    {
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        await CheckCode(code);
        await CheckName(name);
        return new LandType(code, name)
        {
            Description = description,
            OrderIndex = orderIndex,
            Status = status
        };
    }

    public async Task UpdateAsync([NotNull] LandType LandType,
                                  [NotNull] string code,
                                  [NotNull] string name,
                                  string? description,
                                  int? orderIndex,
                                  Status status)
    {
        Check.NotNull(LandType, nameof(LandType));
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        if (LandType.LandTypeCode != code)
        {
            await ChangeCode(LandType, code);
        }
        if (LandType.LandTypeName != name)
        {
            await ChangeName(LandType, name);
        }
        LandType.Description = description;
        LandType.OrderIndex = orderIndex;
        LandType.Status = status;
    }

    private async Task ChangeName(LandType LandType, string name)
    {
        var existedName = await _LandTypeRepo.FindAsync(x => x.LandTypeName == name, false);
        if (existedName != null && existedName.Id != LandType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
        LandType.ChangeName(name);
    }

    private async Task CheckName(string name)
    {
        var existedName = await _LandTypeRepo.FindAsync(x => x.LandTypeName == name, false);
        if (existedName != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
    }

    private async Task ChangeCode(LandType LandType, string code)
    {
        var existedCode = await _LandTypeRepo.FindAsync(x => x.LandTypeCode == code, false);
        if (existedCode != null && existedCode.Id != LandType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
        LandType.ChangeCode(code);
    }

    private async Task CheckCode(string code)
    {
        var existedCode = await _LandTypeRepo.FindAsync(x => x.LandTypeCode == code, false);
        if (existedCode != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
    }
}