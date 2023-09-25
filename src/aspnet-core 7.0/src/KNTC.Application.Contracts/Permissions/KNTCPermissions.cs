namespace KNTC.Permissions;

public static class KNTCPermissions
{
    public const string SysConfigGroupName = "SysConfigs";

    public static class SysConfigsPermission
    {
        public const string Default = SysConfigGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string ComplainGroupName = "Complains";

    public static class ComplainsPermission
    {
        public const string Default = ComplainGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string DenounceGroupName = "Denounces";

    public static class DenouncesPermission
    {
        public const string Default = DenounceGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string DocumentTypeGroupName = "DocumentTypes";

    public static class DocumentTypePermission
    {
        public const string Default = DocumentTypeGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string BaseMapGroupName = "BaseMaps";

    public static class BaseMapPermission
    {
        public const string Default = BaseMapGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string LandTypeGroupName = "LandTypes";

    public static class LandTypePermission
    {
        public const string Default = LandTypeGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string UnitGroupName = "Units";

    public static class UnitPermission
    {
        public const string Default = UnitGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string UnitTypeGroupName = "UnitTypes";

    public static class UnitTypePermission
    {
        public const string Default = UnitTypeGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string SpatialDataGroupName = "SpatialDatas";

    public static class SpatialDatasPermission
    {
        public const string Default = SpatialDataGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string GeoServerGroupName = "GeoServesrs";

    public static class GeoServerPermission
    {
        public const string Default = GeoServerGroupName;
    }

    public const string UserUnitGroupName = "UserUnits";
    public static class UserUnitPermission
    {
        public const string Default = UserUnitGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }    
}