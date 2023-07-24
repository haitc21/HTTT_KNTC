using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.Units;

public class UnitManager : DomainService
{
    private readonly IRepository<Unit, int> _unitRepo;

    public UnitManager(IRepository<Unit, int> unitRepo)
    {
        _unitRepo = unitRepo;
    }

    public async Task<Unit> CreateAsync([NotNull] string code,
                                                [NotNull] string name,
                                                [NotNull] string shortName,
                                                [NotNull] int unitTypeId,
                                                string? description,
                                                int? orderIndex,
                                                Status status)
    {
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(shortName, nameof(shortName));
        Check.NotNull(unitTypeId, nameof(unitTypeId));

        await CheckCode(code);
        await CheckName(name);
        return new Unit(code, name)
        {
            Description = description,
            ShortName = shortName,
            UnitTypeId = unitTypeId,
            OrderIndex = orderIndex,
            Status = status
        };
    }

    public async Task UpdateAsync([NotNull] Unit unit,
                                   [NotNull] string code,
                                   [NotNull] string name,
                                   [NotNull] string shortName,
                                   [NotNull] int unitTypeId,
                                   string? description,
                                   int? orderIndex,
                                   Status status)
    {
        Check.NotNull(unit, nameof(unit));
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(shortName, nameof(shortName));
        Check.NotNull(unitTypeId, nameof(unitTypeId));

        if (unit.UnitCode != code)
        {
            await ChangeCode(unit, code);
        }
        if (unit.UnitName != name)
        {
            await ChangeName(unit, name);
        }
        unit.ShortName = shortName;
        unit.UnitTypeId = unitTypeId;
        unit.Description = description;
        unit.OrderIndex = orderIndex;
        unit.Status = status;
    }

    private async Task ChangeName(Unit unit, string name)
    {
        var existedName = await _unitRepo.FindAsync(x => x.UnitName == name, false);
        if (existedName != null && existedName.Id != unit.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
        unit.ChangeName(name);
    }

    private async Task CheckName(string name)
    {
        var existedName = await _unitRepo.FindAsync(x => x.UnitName == name, false);
        if (existedName != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
    }

    private async Task ChangeCode(Unit unit, string code)
    {
        var existedCode = await _unitRepo.FindAsync(x => x.UnitCode == code, false);
        if (existedCode != null && existedCode.Id != unit.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
        unit.ChangeCode(code);
    }

    private async Task CheckCode(string code)
    {
        var existedCode = await _unitRepo.FindAsync(x => x.UnitCode == code, false);
        if (existedCode != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
    }
}