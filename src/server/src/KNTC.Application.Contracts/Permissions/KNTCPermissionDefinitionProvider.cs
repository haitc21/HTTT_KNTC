using KNTC.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace KNTC.Permissions;

public class KNTCPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var complainGroup = context.AddGroup(KNTCPermissions.ComplainGroupName, L("Permission:ComplainsGr"));

        var complainsPermission = complainGroup.AddPermission(KNTCPermissions.ComplainsPermission.Default, L("Permission:Complains"));
        complainsPermission.AddChild(KNTCPermissions.ComplainsPermission.Create, L("Permission:Complains.Create"));
        complainsPermission.AddChild(KNTCPermissions.ComplainsPermission.Edit, L("Permission:Complains.Edit"));
        complainsPermission.AddChild(KNTCPermissions.ComplainsPermission.Delete, L("Permission:Complains.Delete"));

        var denounceGroup = context.AddGroup(KNTCPermissions.DenounceGroupName, L("Permission:DenouncesGr"));

        var denouncesPermission = denounceGroup.AddPermission(KNTCPermissions.DenouncesPermission.Default, L("Permission:Denounces"));
        denouncesPermission.AddChild(KNTCPermissions.DenouncesPermission.Create, L("Permission:Denounces.Create"));
        denouncesPermission.AddChild(KNTCPermissions.DenouncesPermission.Edit, L("Permission:Denounces.Edit"));
        denouncesPermission.AddChild(KNTCPermissions.DenouncesPermission.Delete, L("Permission:Denounces.Delete"));

        var documentTypeGroup = context.AddGroup(KNTCPermissions.DocumentTypeGroupName, L("Permission:DocumentTypeGr"));

        var documentTypePermission = documentTypeGroup.AddPermission(KNTCPermissions.DocumentTypePermission.Default, L("Permission:DocumentType"));
        documentTypePermission.AddChild(KNTCPermissions.DocumentTypePermission.Create, L("Permission:DocumentType.Create"));
        documentTypePermission.AddChild(KNTCPermissions.DocumentTypePermission.Edit, L("Permission:DocumentType.Edit"));
        documentTypePermission.AddChild(KNTCPermissions.DocumentTypePermission.Delete, L("Permission:DocumentType.Delete"));

        var landTypeGroup = context.AddGroup(KNTCPermissions.LandTypeGroupName, L("Permission:LandTypeGr"));

        var landTypePermission = landTypeGroup.AddPermission(KNTCPermissions.LandTypePermission.Default, L("Permission:LandType"));
        landTypePermission.AddChild(KNTCPermissions.LandTypePermission.Create, L("Permission:LandType.Create"));
        landTypePermission.AddChild(KNTCPermissions.LandTypePermission.Edit, L("Permission:LandType.Edit"));
        landTypePermission.AddChild(KNTCPermissions.LandTypePermission.Delete, L("Permission:LandType.Delete"));

        var unitGroup = context.AddGroup(KNTCPermissions.UnitGroupName, L("Permission:UnitGr"));

        var unitPermission = unitGroup.AddPermission(KNTCPermissions.UnitPermission.Default, L("Permission:Unit"));
        unitPermission.AddChild(KNTCPermissions.UnitPermission.Create, L("Permission:Unit.Create"));
        unitPermission.AddChild(KNTCPermissions.UnitPermission.Edit, L("Permission:Unit.Edit"));
        unitPermission.AddChild(KNTCPermissions.UnitPermission.Delete, L("Permission:Unit.Delete"));

        var unitTypeGroup = context.AddGroup(KNTCPermissions.UnitTypeGroupName, L("Permission:UnitTypeGr"));

        var unitTypePermission = unitTypeGroup.AddPermission(KNTCPermissions.UnitTypePermission.Default, L("Permission:UnitType"));
        unitTypePermission.AddChild(KNTCPermissions.UnitTypePermission.Create, L("Permission:UnitType.Create"));
        unitTypePermission.AddChild(KNTCPermissions.UnitTypePermission.Edit, L("Permission:UnitType.Edit"));
        unitTypePermission.AddChild(KNTCPermissions.UnitTypePermission.Delete, L("Permission:UnitType.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<KNTCResource>(name);
    }
}