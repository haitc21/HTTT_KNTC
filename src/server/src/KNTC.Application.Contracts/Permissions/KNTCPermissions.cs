namespace KNTC.Permissions;

public static class KNTCPermissions
{
    public const string HoSoGroupName = "HoSo";

    public static class HoSos
    {
        public const string Default = HoSoGroupName + ".HoSo";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}