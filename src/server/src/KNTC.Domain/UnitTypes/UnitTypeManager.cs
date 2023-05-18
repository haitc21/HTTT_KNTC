using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.CategoryUnitTypes;

public class UnitTypeManager : DomainService
{
    private readonly IRepository<UnitType, int> _unitTypeRepo;

    public UnitTypeManager(IRepository<UnitType, int> unitTypeRepo)
    {
        _unitTypeRepo = unitTypeRepo;
    }

    public async Task<UnitType> CreateAsync([NotNull] string code,
                                                [NotNull] string name,
                                                string description,
                                                int orderIndex,
                                                Status status)
    {
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        await CheckCode(code);
        await CheckName(name);
        return new UnitType(code, name)
        {
            Description = description,
            OrderIndex = orderIndex,
            Status = status
        };
    }

    public async Task UpdateAsync([NotNull] UnitType unitType,
                                   [NotNull] string code,
                                   [NotNull] string name,
                                   string description,
                                   int orderIndex,
                                   Status status)
    {
        Check.NotNull(unitType, nameof(unitType));
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        if (unitType.UnitTypeCode != code)
        {
            await ChangeCode(unitType, code);
        }
        if (unitType.UnitTypeName != name)
        {
            await ChangeName(unitType, name);
        }
        unitType.Description = description;
        unitType.OrderIndex = orderIndex;
        unitType.Status = status;
    }

    private async Task ChangeName(UnitType unitType, string name)
    {
        var existedName = await _unitTypeRepo.FindAsync(x => x.UnitTypeName == name, false);
        if (existedName != null && existedName.Id != unitType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
        unitType.ChangeName(name);
    }

    private async Task CheckName(string name)
    {
        var existedName = await _unitTypeRepo.FindAsync(x => x.UnitTypeName == name, false);
        if (existedName != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
    }

    private async Task ChangeCode(UnitType unitType, string code)
    {
        var existedCode = await _unitTypeRepo.FindAsync(x => x.UnitTypeCode == code, false);
        if (existedCode != null && existedCode.Id != unitType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
        unitType.ChangeCode(code);
    }

    private async Task CheckCode(string code)
    {
        var existedCode = await _unitTypeRepo.FindAsync(x => x.UnitTypeCode == code, false);
        if (existedCode != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
    }
}