using System.Text.RegularExpressions;

namespace KNTC.Helpers;

public class CkEditorHelper
{
    public static string ConvertToPlainText(string htmlInput)
    {
        string plainText = Regex.Replace(htmlInput, "<.*?>", m =>
        {
            if (m.Value.StartsWith("<p>"))
            {
                return ". ";
            }
            return string.Empty;
        });

        // Replace multiple whitespaces with a single space
        plainText = Regex.Replace(plainText, @"\s+", " ").Trim();

        return plainText;
    }
}