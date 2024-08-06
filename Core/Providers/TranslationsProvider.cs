using System.Collections.ObjectModel;
using System.IO.Abstractions;
using Core.Configuration;
using Core.Extensions;
using Core.Providers.Interfaces;
using Newtonsoft.Json;

namespace Core.Providers;

public class TranslationsProvider : ITranslationsProvider
{
    private readonly Dictionary<string, string> _translations = new();
    private readonly IFileSystem _fileSystem;

    public TranslationsProvider(string lang) : this(new LocaleOptions { Lang = lang }) { }

    public TranslationsProvider(IConfigurationProvider configurationProvider)
        : this(configurationProvider.Get<LocaleOptions>())
    { }

    internal TranslationsProvider(IFileSystem fileSystem, IConfigurationProvider configurationProvider)
    {
        _fileSystem = fileSystem;
        Initialize(configurationProvider.Get<LocaleOptions>());
    }

    private TranslationsProvider(LocaleOptions? localeOptions)
    {
        _fileSystem = new FileSystem();
        Initialize(localeOptions);
    }

    private void Initialize(LocaleOptions? localeOptions)
    {
        ArgumentNullException.ThrowIfNull(localeOptions);

        var translationsFilesForLocale =
            _fileSystem.Directory.GetFiles($"{localeOptions.TranslationsDirectory}", $"*.{localeOptions.Lang}.json");

        foreach (var file in translationsFilesForLocale)
        {
            var jsonContent = _fileSystem.File.ReadAllText(file);
            _translations.AddRange(JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent));
        }
    }

    public string Get(string translationKey) =>
        _translations[translationKey];

    public ReadOnlyDictionary<string, string> GetAll() =>
        new(_translations);
}