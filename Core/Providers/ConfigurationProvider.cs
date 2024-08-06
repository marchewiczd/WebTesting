using Core.Configuration;
using Microsoft.Extensions.Configuration;
using IConfigurationProvider = Core.Providers.Interfaces.IConfigurationProvider;

namespace Core.Providers;

public class ConfigurationProvider : IConfigurationProvider
{
    private readonly IConfigurationRoot _configurationRoot;
    private const string ConfigFileName = "appsettings.json";

    public ConfigurationProvider()
        : this(new ConfigurationBuilder().AddJsonFile(ConfigFileName).Build())
    { }

    internal ConfigurationProvider(IConfigurationRoot configurationRoot)
    {
        _configurationRoot = configurationRoot;
    }

    public T? Get<T>() where T : new()
    {
        if (typeof(T) != typeof(AppConfiguration))
            return _configurationRoot.GetRequiredSection(typeof(T).Name).Get<T>();

        var appConfiguration = new T();
        _configurationRoot.Bind(appConfiguration);
        return appConfiguration;
    }
}