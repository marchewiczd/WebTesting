namespace Core.Extensions;

public static class StringExtensions
{
    public static string NormalizePriceText(this string text) =>
        text.Replace("zł", string.Empty).Replace(" ", string.Empty);
}