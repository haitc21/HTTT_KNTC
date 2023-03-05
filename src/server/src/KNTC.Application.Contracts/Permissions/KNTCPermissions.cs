namespace KNTC.Permissions;

public static class KNTCPermissions
{
    public const string ComplainGroupName = "Complains";

    public static class Complains
    {
        public const string Default = ComplainGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string DenounceGroupName = "Denounces";

    public static class Denounces
    {
        public const string Default = DenounceGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}