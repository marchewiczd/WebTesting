namespace Core.Extensions;

public static class DictionaryExtensions
{
    public static void AddRange(this Dictionary<string, string> dictionary, Dictionary<string, string>? values)
    {
        ArgumentNullException.ThrowIfNull(values);

        foreach (var value in values)
        {
            dictionary.Add(value.Key, value.Value);
        }
    }
}