namespace Core.Providers.Interfaces;

public interface IConfigurationProvider
{
    public T? Get<T>() where T : new();
}