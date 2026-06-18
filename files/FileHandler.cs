using System.Collections.Generic;
using System.IO;
using Modes;
using CoreApp;
using FilePreviews;

namespace FileHandlers;

public record FileItem(string Name, string FullPath, bool IsDirectory);

public class FileHandler
{
    private readonly List<FileItem> _files = new();

    public IReadOnlyList<FileItem> Files => Core.Instance.CurrentMode == Mode.Search
            ? _files.Where(f => f.Name.Contains(Core.Instance.KeyInput.InputBuffer, StringComparison.OrdinalIgnoreCase)).ToList()
            : _files;

    public FilePreview FilePreview { get; private set; } = new FilePreview();

    public void AddFiles(string path)
    {
        var dir = new DirectoryInfo(path);

        if (dir.Parent != null)
        {
            _files.Add(new FileItem("..", dir.Parent.FullName, true));
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

    public void RenameFile(FileItem item, string newName)
    {
        string directory = Path.GetDirectoryName(item.FullPath);
        string newFullPath = Path.Combine(directory, newName);

        if (item.IsDirectory)
        {
            Directory.Move(item.FullPath, newFullPath);
        }
        else
        {
            File.Move(item.FullPath, newFullPath);
        }
    }

    public void ClearFiles()
    {
        _files.Clear();
    }
}
