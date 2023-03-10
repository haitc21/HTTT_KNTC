using KNTC.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace KNTC.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(KNTCEntityFrameworkCoreModule),
    typeof(KNTCApplicationContractsModule)
    )]
public class KNTCDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}