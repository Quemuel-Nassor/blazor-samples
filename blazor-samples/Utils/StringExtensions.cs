using System.Text;
using System.Text.RegularExpressions;

namespace blazor_samples.Utils;

public static class StringExtensions
{
    private static Regex RegxRemoveAccent = new Regex(@"\p{Mn}", RegexOptions.Compiled);
    private static Regex RegxNormalizeSpace = new Regex(@"\s+", RegexOptions.Compiled);
    private static Regex RegxRemoveInvalidChars = new Regex(@"[^\d\w\s]|[_]", RegexOptions.Compiled);

    public static string Slug(this string input)
    {
        if (String.IsNullOrWhiteSpace(input)) return input;

        string withoutInvalidChars = RegxRemoveInvalidChars.Replace(input, " ");
        string normalizedWhiteSpaces = RegxNormalizeSpace.Replace(withoutInvalidChars, " ");
        string withoutAccent = RegxRemoveAccent.Replace(normalizedWhiteSpaces.Normalize(NormalizationForm.FormD), "");
        string normalizedContent = withoutAccent.ToLower().Trim().Replace(" ", "-");

        return normalizedContent;
    }
}
