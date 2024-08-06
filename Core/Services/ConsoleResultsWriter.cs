using Core.Services.Interfaces;

namespace Core.Services;

public class ConsoleResultsWriter : IResultsWriter
{
    public void WriteResults<T>(T results) where T : new()
    {
        ArgumentNullException.ThrowIfNull(results);

        Console.WriteLine(results.ToString());
    }
}