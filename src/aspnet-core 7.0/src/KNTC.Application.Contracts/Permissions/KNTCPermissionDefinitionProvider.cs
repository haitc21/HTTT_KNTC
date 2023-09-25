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
        var documentTypePermission = documentTypeGroup.AddPermission(KNTCPermissions.DocumentTypePermission.Default, L("Permission:DocumentTypes"));
        documentTypePermission.AddChild(KNTCPermissions.DocumentTypePermission.Create, L("Permission:DocumentTypes.Create"));
        documentTypePermission.AddChild(KNTCPermissions.DocumentTypePermission.Edit, L("Permission:DocumentTypes.Edit"));
        documentTypePermission.AddChild(KNTCPermissions.DocumentTypePermission.Delete, L("Permission:DocumentTypes.Delete"));

        var landTypeGroup = context.AddGroup(KNTCPermissions.LandTypeGroupName, L("Permission:LandTypeGr"));
        var landTypePermission = landTypeGroup.AddPermission(KNTCPermissions.LandTypePermission.Default, L("Permission:LandTypes"));
        landTypePermission.AddChild(KNTCPermissions.LandTypePermission.Create, L("Permission:LandTypes.Create"));
        landTypePermission.AddChild(KNTCPermissions.LandTypePermission.Edit, L("Permission:LandTypes.Edit"));
        landTypePermission.AddChild(KNTCPermissions.LandTypePermission.Delete, L("Permission:LandTypes.Delete"));

        var unitGroup = context.AddGroup(KNTCPermissions.UnitGroupName, L("Permission:UnitGr"));
        var unitPermission = unitGroup.AddPermission(KNTCPermissions.UnitPermission.Default, L("Permission:Units"));
        unitPermission.AddChild(KNTCPermissions.UnitPermission.Create, L("Permission:Units.Create"));
        unitPermission.AddChild(KNTCPermissions.UnitPermission.Edit, L("Permission:Units.Edit"));
        unitPermission.AddChild(KNTCPermissions.UnitPermission.Delete, L("Permission:Units.Delete"));

        var unitTypeGroup = context.AddGroup(KNTCPermissions.UnitTypeGroupName, L("Permission:UnitTypeGr"));
        var unitTypePermission = unitTypeGroup.AddPermission(KNTCPermissions.UnitTypePermission.Default, L("Permission:UnitTypes"));
        unitTypePermission.AddChild(KNTCPermissions.UnitTypePermission.Create, L("Permission:UnitTypes.Create"));
        unitTypePermission.AddChild(KNTCPermissions.UnitTypePermission.Edit, L("Permission:UnitTypes.Edit"));
        unitTypePermission.AddChild(KNTCPermissions.UnitTypePermission.Delete, L("Permission:UnitTypes.Delete"));

        var baseMapGroup = context.AddGroup(KNTCPermissions.BaseMapGroupName, L("Permission:BaseMapGr"));
        var baseMapPermission = baseMapGroup.AddPermission(KNTCPermissions.BaseMapPermission.Default, L("Permission:BaseMaps"));
        baseMapPermission.AddChild(KNTCPermissions.BaseMapPermission.Create, L("Permission:BaseMaps.Create"));
        baseMapPermission.AddChild(KNTCPermissions.BaseMapPermission.Edit, L("Permission:BaseMaps.Edit"));
        baseMapPermission.AddChild(KNTCPermissions.BaseMapPermission.Delete, L("Permission:BaseMaps.Delete"));

        var geoServerGroup = context.AddGroup(KNTCPermissions.GeoServerGroupName, L("Permission:GeoServerGr"));
        var geoServerPermission = geoServerGroup.AddPermission(KNTCPermissions.GeoServerPermission.Default, L("Permission:GeoServesrs"));

        var configGroup = context.AddGroup(KNTCPermissions.SysConfigGroupName, L("Permission:SysConfigGr"));
        var configPermission = configGroup.AddPermission(KNTCPermissions.SysConfigsPermission.Default, L("Permission:SysConfigs"));
        configPermission.AddChild(KNTCPermissions.SysConfigsPermission.Create, L("Permission:SysConfigs.Create"));
        configPermission.AddChild(KNTCPermissions.SysConfigsPermission.Edit, L("Permission:SysConfigs.Edit"));
        configPermission.AddChild(KNTCPermissions.SysConfigsPermission.Delete, L("Permission:SysConfigs.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<KNTCResource>(name);
    }
}