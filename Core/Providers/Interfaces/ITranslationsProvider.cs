namespace Core.Providers.Interfaces;

public interface ITranslationsProvider
{
    public string Get(string translationKey);
}