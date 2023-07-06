using KNTC.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
//using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Timing;

//using Volo.Abp.TenantManagement;

namespace KNTC;

[DependsOn(
    typeof(KNTCDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(KNTCApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    //typeof(AbpTenantManagementApplicationModule),
    //typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBlobStoringFileSystemModule)
    )]
public class KNTCApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var basePath = configuration["FileSystemBasePath"];
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<KNTCApplicationModule>();
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("KNTC", typeof(KNTCResource));
        });
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseFileSystem(fileSys =>
                {
                    fileSys.BasePath = basePath;
                });
            });
        });
        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }
}