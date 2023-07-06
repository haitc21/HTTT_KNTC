﻿using KNTC.Localization;
using Localization.Resources.AbpUi;
using System;
using Volo.Abp.Account;
//using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.Timing;

//using Volo.Abp.TenantManagement;

namespace KNTC;

[DependsOn(
    typeof(KNTCApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    //typeof(AbpTenantManagementHttpApiModule),
    //typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule)
    )]
public class KNTCHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<KNTCResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}