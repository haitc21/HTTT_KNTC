using KNTC.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

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


        var documentTypeGroup = context.AddGroup(KNTCPermissions.DocumentTypeGroupName, L("Permission:DocumentType"));

        var documentTypePermission = documentTypeGroup.AddPermission(KNTCPermissions.DocumentType.Default, L("Permission:DocumentType"));
        documentTypePermission.AddChild(KNTCPermissions.DocumentType.Create, L("Permission:DocumentType.Create"));
        documentTypePermission.AddChild(KNTCPermissions.DocumentType.Edit, L("Permission:DocumentType.Edit"));
        documentTypePermission.AddChild(KNTCPermissions.DocumentType.Delete, L("Permission:DocumentType.Delete"));

        var landTypeGroup = context.AddGroup(KNTCPermissions.LandTypeGroupName, L("Permission:LandType"));

        var landTypePermission = landTypeGroup.AddPermission(KNTCPermissions.LandType.Default, L("Permission:LandType"));
        landTypePermission.AddChild(KNTCPermissions.LandType.Create, L("Permission:LandType.Create"));
        landTypePermission.AddChild(KNTCPermissions.LandType.Edit, L("Permission:LandType.Edit"));
        landTypePermission.AddChild(KNTCPermissions.LandType.Delete, L("Permission:LandType.Delete"));


        var unitGroup = context.AddGroup(KNTCPermissions.UnitGroupName, L("Permission:Unit"));

        var unitPermission = unitGroup.AddPermission(KNTCPermissions.Unit.Default, L("Permission:Unit"));
        unitPermission.AddChild(KNTCPermissions.Unit.Create, L("Permission:Unit.Create"));
        unitPermission.AddChild(KNTCPermissions.Unit.Edit, L("Permission:Unit.Edit"));
        unitPermission.AddChild(KNTCPermissions.Unit.Delete, L("Permission:Unit.Delete"));

        var unitTypeGroup = context.AddGroup(KNTCPermissions.UnitTypeGroupName, L("Permission:UnitType"));

        var unitTypePermission = unitTypeGroup.AddPermission(KNTCPermissions.UnitType.Default, L("Permission:UnitType"));
        unitTypePermission.AddChild(KNTCPermissions.UnitType.Create, L("Permission:UnitType.Create"));
        unitTypePermission.AddChild(KNTCPermissions.UnitType.Edit, L("Permission:UnitType.Edit"));
        unitTypePermission.AddChild(KNTCPermissions.UnitType.Delete, L("Permission:UnitType.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<KNTCResource>(name);
    }
}