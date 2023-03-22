using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FluentValidation;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
//using Volo.Abp.TenantManagement;

namespace KNTC;

[DependsOn(
    typeof(KNTCDomainSharedModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    //typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpObjectExtendingModule),
    typeof(AbpFluentValidationModule)
)]
[DependsOn(typeof(AbpFluentValidationModule))]
public class KNTCApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        KNTCDtoExtensions.Configure();
    }
}