namespace Core.Services.Interfaces;

public interface IResultsWriter
{
    public void WriteResults<T>(T results) where T : new();
}