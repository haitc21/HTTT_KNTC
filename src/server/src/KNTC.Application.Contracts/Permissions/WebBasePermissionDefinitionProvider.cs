using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using KNTC.Localization;

namespace KNTC.Permissions;

public class KNTCPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(KNTCPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(KNTCPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<KNTCResource>(name);
    }
}