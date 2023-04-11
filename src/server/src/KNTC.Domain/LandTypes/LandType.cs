using KNTC.Complains;
using KNTC.Denounces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.LandTypes;

public class LandType : AuditedAggregateRoot<int>
{
    public LandType()
    {

    }
    public LandType(int id) : base(id)
    {

    }
    public LandType(string code, string name)
    {
        ChangeCode(code);
        ChangeName(name);
    }
    public LandType(int id, string code, string name) : base(id)
    {
        ChangeCode(code);
        ChangeName(name);
    }
    public string LandTypeCode { get; private set; }
    public string LandTypeName { get; private set; }
    public string Description { get; set; }
    public int? OrderIndex { get; set; }
    public Status Status { get; set; }
    public virtual List<Complain> Complains { get; set; }
    public virtual List<Denounce> Denounces { get; set; }
    private void SetCode([NotNull] string code)
    {
        LandTypeCode = Check.NotNullOrWhiteSpace(
            code,
            nameof(code),
            maxLength: KNTCValidatorConsts.MaxCodeLength
        );
    }

    internal LandType ChangeCode([NotNull] string code)
    {
        SetCode(code);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        LandTypeName = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: KNTCValidatorConsts.MaxNameLength
        );
    }

    internal LandType ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }
}
