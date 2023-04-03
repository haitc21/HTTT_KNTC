using JetBrains.Annotations;
using KNTC.Configs;
using KNTC.CategoryUnitTypes;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.Units;

public class Unit : FullAuditedAggregateRoot<int>
{
    public Unit()
    {

    }
    public Unit(int id) : base(id)
    {

    }
    public Unit(string code, string name)
    {
        ChangeCode(code);
        ChangeName(name);
    }
    public Unit(int id, string code, string name) : base(id)
    {
        ChangeCode(code);
        ChangeName(name);
    }
    public string UnitCode { get; private set; }
    public string UnitName { get; private set; }
    public string ShortName { get; set; }
    public int UnitTypeId { get; set; }
    public int? ConfigId { get; set; }
    public int? ParentId { get; set; }
    public string Description { get; set; }
    public int? OrderIndex { get; set; }
    public Status Status { get; set; }
    public UnitType UnitType { get; set; }
    private void SetCode([NotNull] string code)
    {
        UnitCode = Check.NotNullOrWhiteSpace(
            code,
            nameof(code),
            maxLength: KNTCValidatorConsts.MaxCodeLength
        );
    }

    internal Unit ChangeCode([NotNull] string code)
    {
        SetCode(code);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        UnitName = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: KNTCValidatorConsts.MaxNameLength
        );
    }

    internal Unit ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

}
