using KNTC.Units;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.Configs;

public class Config : FullAuditedAggregateRoot<int>
{
    public Config()
    {

    }
    public Config(int id) : base(id)
    {

    }
    public Config(string code, string name)
    {
        ChangeCode(code);
        ChangeName(name);
    }
    public Config(int id, string code, string name) : base(id)
    {
        ChangeCode(code);
        ChangeName(name);
    }
    public string OrganizationCode { get; private set; }
    public string OrganizationName { get; private set; }
    
    public string ToaDo { get; set; }
    public string Tel { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    //public virtual List<Unit> Units { get; set; }
    private void SetCode([NotNull] string code)
    {
        OrganizationCode = Check.NotNullOrWhiteSpace(
            code,
            nameof(code),
            maxLength: KNTCValidatorConsts.MaxCodeLength
        );
    }

    internal Config ChangeCode([NotNull] string code)
    {
        SetCode(code);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        OrganizationName = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: KNTCValidatorConsts.MaxNameLength
        );
    }

    internal Config ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }
    private void SetToaDo([NotNull] string toado)
    {
        ToaDo = Check.NotNullOrWhiteSpace(
            toado,
            nameof(toado)
        );
    }

    internal Config ChangeToaDo([NotNull] string toado)
    {
        SetCode(toado);
        return this;
    }
}
