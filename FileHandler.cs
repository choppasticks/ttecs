using System.Collections.Generic;
using System.IO;

namespace FileHandlers;

public record FileItem(string Name, string FullPath, bool IsDirectory);

public class FileHandler
{
    private readonly List<FileItem> _files = new();

    public IReadOnlyList<FileItem> Files => _files;

    public void AddFiles(string path)
    {
        var dir = new DirectoryInfo(path);

        if (dir.Parent != null)
        {
            _files.Add(new FileItem("...", dir.Parent.FullName, true));
        }

        foreach (var subDir in dir.GetDirectories())
        {
            _files.Add(new FileItem(subDir.Name, subDir.FullName, true));
        }

        foreach (var file in dir.GetFiles())
        {
            _files.Add(new FileItem(file.Name, file.FullName, false));
        }
    }

    public void ClearFiles()
    {
        _files.Clear();
    }
}
