using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace KNTC;

[Dependency(ReplaceServices = true)]
public class KNTCBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "KNTC";
}