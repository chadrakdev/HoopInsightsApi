namespace HoopInsightsAPI.Extensions;

public static class StringExtensions
{
    public static string NormaliseForFuzzySearch(this string value)
    {
        return Uri.EscapeDataString(value.Trim().ToLowerInvariant());
    }
}