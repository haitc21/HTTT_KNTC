using KNTC.EntityFrameworkCore;
using System;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

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

        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }
}