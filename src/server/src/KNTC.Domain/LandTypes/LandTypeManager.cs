using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.LandTypes;

public class LandTypeManager : DomainService
{
    private readonly IRepository<LandType, int> _landTypeRepo;
    public LandTypeManager(IRepository<LandType, int> landTypeRepo)
    {
        _landTypeRepo = landTypeRepo;
    }
    public async Task<LandType> CreateAsync([NotNull] string code,
                                                [NotNull] string name,
                                                string description,
                                                int orderIndex,
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
    public async Task UpdateAsync([NotNull] LandType landType,
                                  [NotNull] string code,
                                  [NotNull] string name,
                                  string description,
                                  int orderIndex,
                                  Status status)
    {
        Check.NotNull(landType, nameof(landType));
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        if (landType.LandTypeCode != code)
        {
            await ChangeCode(landType, code);
        }
        if (landType.LandTypeName != name)
        {
            await ChangeName(landType, name);
        }
        landType.Description = description;
        landType.OrderIndex = orderIndex;
        landType.Status = status;
    }
    private async Task ChangeName(LandType landType, string name)
    {
        var existedName = await _landTypeRepo.FindAsync(x => x.LandTypeName == name, false);
        if (existedName != null && existedName.Id != landType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
        landType.ChangeName(name);
    }
    private async Task CheckName(string name)
    {
        var existedName = await _landTypeRepo.FindAsync(x => x.LandTypeName == name, false);
        if (existedName != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
    }
    private async Task ChangeCode(LandType landType, string code)
    {
        var existedCode = await _landTypeRepo.FindAsync(x => x.LandTypeCode == code, false);
        if (existedCode != null && existedCode.Id != landType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
        landType.ChangeCode(code);
    }
    private async Task CheckCode(string code)
    {
        var existedCode = await _landTypeRepo.FindAsync(x => x.LandTypeCode == code, false);
        if (existedCode != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
    }
}
