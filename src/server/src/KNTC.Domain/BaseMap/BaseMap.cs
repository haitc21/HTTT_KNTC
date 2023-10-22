using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.BaseMaps;

public class BaseMap : AuditedAggregateRoot<int>
{
    public BaseMap()
    {
    }

    public BaseMap(string code, string name)
    {
        ChangeCode(code);
        ChangeName(name);
    }

    public string BaseMapCode { get; private set; }
    public string BaseMapName { get; private set; }
    public string? Description { get; set; }
    public int? OrderIndex { get; set; }
    public Status Status { get; set; }

    private void SetCode([NotNull] string code)
    {
        BaseMapCode = Check.NotNullOrWhiteSpace(
            code,
            nameof(code),
            maxLength: KNTCValidatorConsts.MaxCodeLength
        );
    }

    internal BaseMap ChangeCode([NotNull] string code)
    {
        SetCode(code);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        BaseMapName = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: KNTCValidatorConsts.MaxNameLength
        );
    }

    internal BaseMap ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }
}