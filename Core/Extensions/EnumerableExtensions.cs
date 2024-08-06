namespace Core.Extensions;

public static class EnumerableExtensions
{
    public static double AverageRounded<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, double> selector,
        int decimals = 2)
    {
        ArgumentNullException.ThrowIfNull(source);
        return Math.Round(source.Average(selector), decimals);
    }
}