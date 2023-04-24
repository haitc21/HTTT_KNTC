//using KNTC.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Timing;

namespace KNTC;

[DependsOn(
    typeof(KNTCDomainSharedModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpBackgroundJobsDomainModule),
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpOpenIddictDomainModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpSettingManagementDomainModule),
    //typeof(AbpTenantManagementDomainModule),
    typeof(AbpEmailingModule),
    typeof(AbpCachingStackExchangeRedisModule)
)]
public class KNTCDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
        //Configure<AbpMultiTenancyOptions>(options =>
        //{
        //    options.IsEnabled = MultiTenancyConsts.IsEnabled;
        //});

        //Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "KNTC:"; });

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
    }
}