using KNTC.Units;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.UnitTypes;

public class UnitType : FullAuditedAggregateRoot<int>
{
    public UnitType()
    {

    }
    public UnitType(int id) : base(id)
    {

    }
    public UnitType(string code, string name)
    {
        ChangeCode(code);
        ChangeName(name);
    }
    public UnitType(int id, string code, string name) : base(id)
    {
        ChangeCode(code);
        ChangeName(name);
    }
    public string UnitTypeCode { get; private set; }
    public string UnitTypeName { get; private set; }
    public string Description { get; set; }
    public int? OrderIndex { get; set; }
    public Status Status { get; set; }
    public virtual List<Unit> Units { get; set; }
    private void SetCode([NotNull] string code)
    {
        UnitTypeCode = Check.NotNullOrWhiteSpace(
            code,
            nameof(code),
            maxLength: KNTCValidatorConsts.MaxCodeLength
        );
    }

    internal UnitType ChangeCode([NotNull] string code)
    {
        SetCode(code);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        UnitTypeName = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: KNTCValidatorConsts.MaxNameLength
        );
    }

    internal UnitType ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }
}
