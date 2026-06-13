using System.Collections.Generic;
using System.IO;

namespace FileHandlers;

public class FileHandler
{
    private readonly List<FileSystemInfo> _files = new();

    public IReadOnlyList<FileSystemInfo> Files => _files;

    public void AddFiles(string path)
    {
        var dir = new DirectoryInfo(path);
        _files.AddRange(dir.GetFiles());
        _files.AddRange(dir.GetDirectories());
    }

    public void ClearFiles()
    {
        _files.Clear();
    }
}
