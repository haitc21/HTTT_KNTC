using KNTC.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace KNTC;

[DependsOn(
    typeof(KNTCEntityFrameworkCoreTestModule)
    )]
public class KNTCDomainTestModule : AbpModule
{

}
