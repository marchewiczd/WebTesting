using System.IO.Abstractions;
using Core.Services.Interfaces;

namespace Core.Services;

public class FileResultsWriter : IResultsWriter
{
    private readonly string _fileName;
    private readonly IFileSystem _fileSystem;

    internal FileResultsWriter(IFileSystem fileSystem, string fileName)
    {
        _fileSystem = fileSystem;
        _fileName = fileName;
    }

    public FileResultsWriter(string fileName) : this(new FileSystem(), fileName) { }

    public void WriteResults<T>(T results) where T : new()
    {
        ArgumentNullException.ThrowIfNull(results);

        if (_fileSystem.File.Exists(_fileName))
        {
            _fileSystem.File.AppendAllText(_fileName, $"\n{results}");
        }
        else
        {
            _fileSystem.File.WriteAllText(_fileName, results.ToString());
        }
    }
}