using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using KNTC.Localization;

namespace KNTC.Permissions;

public class KNTCPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var hoSoGroup = context.AddGroup(KNTCPermissions.HoSoGroupName, L("Permission:HoSos"));

        var HoSosPermission = hoSoGroup.AddPermission(KNTCPermissions.HoSos.Default, L("Permission:HoSos"));
        HoSosPermission.AddChild(KNTCPermissions.HoSos.Create, L("Permission:HoSos.Create"));
        HoSosPermission.AddChild(KNTCPermissions.HoSos.Edit, L("Permission:HoSos.Edit"));
        HoSosPermission.AddChild(KNTCPermissions.HoSos.Delete, L("Permission:HoSos.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<KNTCResource>(name);
    }
}