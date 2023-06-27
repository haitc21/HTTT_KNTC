using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.SysConfigs;

public class SysConfig : AuditedAggregateRoot<int>
{
    public SysConfig()
    {
    }

    public SysConfig(int id) : base(id)
    {
    }

    public SysConfig(string name, string value)
    {
        ChangeName(name);
        Value = value;
    }
    public string Name { get; private set; }
    public string Value { get; set; }
    public string Description { get; set; }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: KNTCValidatorConsts.MaxNameLength
        );
    }

    internal SysConfig ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }
}