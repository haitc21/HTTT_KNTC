using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace KNTC;

[Dependency(ReplaceServices = true)]
public class KNTCBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "KNTC";
}
