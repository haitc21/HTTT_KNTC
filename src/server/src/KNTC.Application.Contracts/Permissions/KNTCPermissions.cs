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

    public const string DocumentTypeGroupName = "DocumentType";

    public static class DocumentType
    {
        public const string Default = DocumentTypeGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public const string LandTypeGroupName = "LandType";

    public static class LandType
    {
        public const string Default = LandTypeGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string UnitGroupName = "Unit";

    public static class Unit
    {
        public const string Default = UnitGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public const string UnitTypeGroupName = "UnitType";

    public static class UnitType
    {
        public const string Default = UnitTypeGroupName;
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}