namespace KNTC;

public static class KNTCConsts
{
    public const string DbTablePrefix = "App";

    public const string KNTCDbSchema = "KNTC";
}

public struct RegexDefine
{
    public const string Email = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    public const string Year = @"^\d{4}$";
    public const string IntergerDuong = @"^\d*$";//"^0|([1-9]+[0-9]*)$";
    public const string Interger = @"^((\-\d+)|(\d*))$";//@"^0|(\-?[1-9]+[0-9]*)$";
    public const string NumericDuong = @"^(\d+(\.\d+)?)$";
    public const string Numeric = @"^(\-?\d+(\.\d+)?)$";
    public const string PhoneNumber = @"^(\+?[0-9\s\-\.]{9,15})$";
    public const string AscII = @"^([a-zA-Z\s]+)$";
    public const string Unicode = "^([\u00c0-\u020f\u1ea0-\u1ef9a-zA-Z0-9_\\-\\.\\s]*)$";
    public const string Code = @"^[a-zA-Z0-9_\-\.]+$";
    public const string CodeVN = "^[Đđa-zA-Z0-9_\\-\\.]+$";
    public const string CodeVNNosign = "^[ĂÂĐÊÔƠƯăâđêôơưa-zA-Z0-9_\\-\\.]+$";
    public const string CodeVNUnicode = "^[\u00c0-\u020fa-zA-Z0-9_\\-\\.]+$";
    public const string LstCodeVN = "^[\u00c0-\u020fa-zA-Z0-9_\\-\\.\\,]+$";
    public const string CharacterNumber = @"^[a-zA-Z0-9]+$";
    public const string CardNumber = @"^[a-zA-Z0-9_ \-\.]+$";
    public const string DateVN = @"^((((31\/(0?[13578]|1[02]))|((29|30)\/(0?[1,3-9]|1[0-2])))\/(1[6-9]|[2-9]\d)?\d{2})|(29\/0?2\/(((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))|(0?[1-9]|1\d|2[0-8])\/((0?[1-9])|(1[0-2]))\/((1[6-9]|[2-9]\d)?\d{2}))$";
    public const string DateVN_Null = @"^(((((31\/(0?[13578]|1[02]))|((29|30)\/(0?[1,3-9]|1[0-2])))\/(1[6-9]|[2-9]\d)?\d{2})|(29\/0?2\/(((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))|(0?[1-9]|1\d|2[0-8])\/((0?[1-9])|(1[0-2]))\/((1[6-9]|[2-9]\d)?\d{2}))|(__\/__\/____))$";
    public const string DateTimeVN = @"^((((31\/(0?[13578]|1[02]))|((29|30)\/(0?[1,3-9]|1[0-2])))\/(1[6-9]|[2-9]\d)?\d{2})|(29\/0?2\/(((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))|(0?[1-9]|1\d|2[0-8])\/((0?[1-9])|(1[0-2]))\/((1[6-9]|[2-9]\d)?\d{2}))\s([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
    public const string DateIso = @"^$";
    public const string MaSoThue = @"^([a-zA-Z0-9\s\-]*)$";
    public const string Time24 = "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
    public const string Time12VN = "^(0?[0-9]|1[0-2]):[0-5][0-9] (SA|CH)$";
    public const string Time12EN = "^(0?[0-9]|1[0-2]):[0-5][0-9] (AM|PM)$";
    public const string Guid = "^\\{?[a-fA-F\\d]{8}-([a-fA-F\\d]{4}-){3}[a-fA-F\\d]{12}\\}?$";
    public const string Month = "^(0?[1-9]|1[012])/\\d\\d\\d\\d";
}

public struct FormatType
{
    public const string FormatDateVN = "dd/MM/yyyy";
    public const string FormatMonthVN = "MM/yyyy";
    public const string FormatDateTimeVN = "dd/MM/yyyy HH:mm";
    public const string FormatTime = "HH:mm";
    public const string Currency = "##,#.## đ";
    public const string TrongLuong = "##,#.## g";
    public const string Percent = "##,#.##\\%";
    public const string Integer = "##,#";
    public const string Number = "##,#.##";
}