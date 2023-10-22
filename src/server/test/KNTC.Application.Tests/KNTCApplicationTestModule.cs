using Volo.Abp.Modularity;

namespace KNTC;

[DependsOn(
    typeof(KNTCApplicationModule),
    typeof(KNTCDomainTestModule)
    )]
public class KNTCApplicationTestModule : AbpModule
{
}