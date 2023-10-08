using KNTC.Roles;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace KNTC.EntityFrameworkCore;

public static class KNTCEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        KNTCGlobalFeatureConfigurator.Configure();
        KNTCModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                           .AddOrUpdateProperty<IdentityRole, string>(
                            RoleConsts.DescriptionFieldName,
                            options =>
                            {
                                options.Attributes.Add(new RequiredAttribute());
                                options.Attributes.Add(new StringLengthAttribute(RoleConsts.DescriptionMaxLength));
                            });
        });
    }
}