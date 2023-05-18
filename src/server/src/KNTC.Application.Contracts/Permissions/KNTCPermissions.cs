namespace KNTC.Permissions;

public static class KNTCPermissions
{
    public const string ConfigGroupName = "Configs";

    public static class ConfigsPermission
    {
        public const string Default = ConfigGroupName;
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

    public const string DocumentTypeGroupName = "DocumentType";

    public static class DocumentTypePermission
    {
        public const string Default = DocumentTypeGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string LandTypeGroupName = "LandType";

    public static class LandTypePermission
    {
        public const string Default = LandTypeGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string UnitGroupName = "Unit";

    public static class UnitPermission
    {
        public const string Default = UnitGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string UnitTypeGroupName = "UnitType";

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
}