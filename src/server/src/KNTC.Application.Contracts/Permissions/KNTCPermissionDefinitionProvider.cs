using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using KNTC.Localization;

namespace KNTC.Permissions;

public class KNTCPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var complainGroup = context.AddGroup(KNTCPermissions.ComplainGroupName, L("Permission:Complains"));

        var complainsPermission = complainGroup.AddPermission(KNTCPermissions.Complains.Default, L("Permission:Complains"));
        complainsPermission.AddChild(KNTCPermissions.Complains.Create, L("Permission:Complains.Create"));
        complainsPermission.AddChild(KNTCPermissions.Complains.Edit, L("Permission:Complains.Edit"));
        complainsPermission.AddChild(KNTCPermissions.Complains.Delete, L("Permission:Complains.Delete"));

        var denounceGroup = context.AddGroup(KNTCPermissions.DenounceGroupName, L("Permission:Denounces"));

        var denouncesPermission = denounceGroup.AddPermission(KNTCPermissions.Denounces.Default, L("Permission:Denounces"));
        denouncesPermission.AddChild(KNTCPermissions.Denounces.Create, L("Permission:Denounces.Create"));
        denouncesPermission.AddChild(KNTCPermissions.Denounces.Edit, L("Permission:Denounces.Edit"));
        denouncesPermission.AddChild(KNTCPermissions.Denounces.Delete, L("Permission:Denounces.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<KNTCResource>(name);
    }
}