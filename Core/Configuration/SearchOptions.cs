namespace Core.Configuration;

public class SearchOptions
{
    public string? SearchPhrase { get; init; }
    public int NumberOfPagesRequested { get; init; }
    public string? Uri { get; init; }
}