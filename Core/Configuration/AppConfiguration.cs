namespace Core.Configuration;

public class AppConfiguration
{
    public SearchOptions? SearchOptions { get; init; }
    public SeleniumDriverOptions? SeleniumDriverOptions { get; init; }
    public LocaleOptions? LocaleOptions { get; init; }
}