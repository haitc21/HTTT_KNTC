using KNTC.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace KNTC.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(KNTCEntityFrameworkCoreModule),
    typeof(KNTCApplicationContractsModule)
    )]
public class KNTCDbMigratorModule : AbpModule
{
}