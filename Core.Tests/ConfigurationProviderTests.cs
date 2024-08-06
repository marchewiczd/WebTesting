using System.Text;
using Core.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using ConfigurationProvider = Core.Providers.ConfigurationProvider;
using IConfigurationProvider = Core.Providers.Interfaces.IConfigurationProvider;

namespace Core.Tests;

public class ConfigurationProviderTests
{
    private const string ConfigFileName = "appsettings.json";

    private readonly SearchOptions _expectedSearchOptions = new()
    {
        SearchPhrase = "Opel Astra",
        Uri = "https://ogloszenia.trojmiasto.pl/motoryzacja-sprzedam/",
        NumberOfPagesRequested = 2
    };

    private readonly LocaleOptions _expectedLocaleOptions = new()
    {
        Lang = "pl_PL",
        TranslationsDirectory = "Translations"
    };

    private readonly SeleniumDriverOptions _expectedSeleniumDriverOptions = new()
    {
        Headless = false
    };

    private AppConfiguration ExpectedAppConfiguration => new()
    {
        SearchOptions = _expectedSearchOptions,
        LocaleOptions = _expectedLocaleOptions,
        SeleniumDriverOptions = _expectedSeleniumDriverOptions
    };

    [Fact]
    public void ConfigProvider_CreatesConfiguration_ReturnsAllObjectsCorrectly()
    {
        var jsonContent =
            $"{{\n\"LocaleOptions\": {{\n\"Lang\": \"{_expectedLocaleOptions.Lang}\",\n\"TranslationsDirectory\": \"{_expectedLocaleOptions.TranslationsDirectory}\"\n}}," +
            $"\n\"SearchOptions\": {{\n\"SearchPhrase\": \"{_expectedSearchOptions.SearchPhrase}\",\n\"NumberOfPagesRequested\": {_expectedSearchOptions.NumberOfPagesRequested},\n\"Uri\":\"{_expectedSearchOptions.Uri}\"\n }}," +
            $"\n\"SeleniumDriverOptions\": {{\n\"Headless\": {_expectedSeleniumDriverOptions.Headless.ToString().ToLowerInvariant()}\n}}\n}}";

        var configurationRoot = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(jsonContent))).Build();

        IConfigurationProvider provider = new ConfigurationProvider(configurationRoot);

        var appCfg = provider.Get<AppConfiguration>();
        var searchCfg = provider.Get<SearchOptions>();
        var seleniumDriverCfg = provider.Get<SeleniumDriverOptions>();
        var localeCfg = provider.Get<LocaleOptions>();

        appCfg.Should().BeOfType(typeof(AppConfiguration)).And.BeEquivalentTo(ExpectedAppConfiguration);
        searchCfg.Should().BeOfType(typeof(SearchOptions)).And.BeEquivalentTo(_expectedSearchOptions);
        seleniumDriverCfg.Should().BeOfType(typeof(SeleniumDriverOptions)).And.BeEquivalentTo(_expectedSeleniumDriverOptions);
        localeCfg.Should().BeOfType(typeof(LocaleOptions)).And.BeEquivalentTo(_expectedLocaleOptions);
    }
}