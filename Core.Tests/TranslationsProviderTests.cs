using System.Collections.ObjectModel;
using System.IO.Abstractions.TestingHelpers;
using Core.Configuration;
using Core.Providers;
using Core.Providers.Interfaces;
using FluentAssertions;
using NSubstitute;

namespace Core.Tests;

public class TranslationsProviderTests
{
    private const string Locale = "en_GB";
    private const string TranslationsDirectoryName = "Translations";
    private const string FileName = $"translations.{Locale}.json";
    private const string OptionalFileName = $"translations2.{Locale}.json";
    private readonly IConfigurationProvider _configurationProvider = Substitute.For<IConfigurationProvider>();

    private readonly Dictionary<string, string> _expectedTranslations = new()
    {
        ["translation.sub_key"] = "test",
        ["translation.sub_key.file2"] = "test_file2"
    };

    public TranslationsProviderTests()
    {
        _configurationProvider.Get<LocaleOptions>().Returns(new LocaleOptions
        {
            Lang = Locale,
            TranslationsDirectory = TranslationsDirectoryName
        });
    }

    [Fact]
    public void TranslationsProvider_ReturnsTranslationsForSpecificLocale()
    {
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {
                Path.Join(TranslationsDirectoryName, FileName),
                new MockFileData($"{{\n\t\"{_expectedTranslations.Keys.First()}\": " +
                                 $"\"{_expectedTranslations.Values.First()}\"\n}}")
            }
        });

        var translations = new TranslationsProvider(fileSystem, _configurationProvider);

        translations.Get(_expectedTranslations.Keys.First())
            .Should().Be(_expectedTranslations.Values.First());
        translations.GetAll().Count.Should().Be(1);
    }

    [Fact]
    public void TranslationsProvider_ReturnsTranslationsFromSpecificLocale_MultipleFiles()
    {
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            { Path.Join(TranslationsDirectoryName, FileName),
                new MockFileData($"{{\n\t\"{_expectedTranslations.Keys.Last()}\": " +
                                 $"\"{_expectedTranslations.Values.Last()}\"\n}}") },
            { Path.Join(TranslationsDirectoryName, OptionalFileName),
                new MockFileData($"{{\n\t\"{_expectedTranslations.Keys.First()}\": " +
                                 $"\"{_expectedTranslations.Values.First()}\"\n}}") }
        });

        var translations = new TranslationsProvider(fileSystem, _configurationProvider);

        translations.Get(_expectedTranslations.Keys.First())
            .Should().Be(_expectedTranslations.Values.First());
        translations.Get(_expectedTranslations.Keys.Last())
            .Should().Be(_expectedTranslations.Values.Last());
        translations.GetAll().Count.Should().Be(2);
        translations.GetAll()
            .Should().BeEquivalentTo(new ReadOnlyDictionary<string, string>(_expectedTranslations));
    }
}