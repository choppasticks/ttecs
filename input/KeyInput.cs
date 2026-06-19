using CoreApp;
using System;
using Modes;
using FileHandlers;

namespace KeyInputs;

public class KeyInput
{
    private int _selectedIndex = 0;

    public int SelectedIndex => _selectedIndex;

    public string InputBuffer { get; set; } = string.Empty;

    public void HandleKeyInput(ConsoleKeyInfo keyInfo, FileHandler fileHandler, string currentPath)
    {
        switch (Core.Instance.CurrentMode)
        {
            case Mode.Browse:
                HandleBrowseInput(keyInfo, fileHandler, currentPath);
                break;
            case Mode.Search:
                HandleTextInput(keyInfo, false, fileHandler, currentPath);
                break;
            case Mode.Rename:
                HandleTextInput(keyInfo, true, fileHandler, currentPath);
                break;
        }
    }

    private void HandleBrowseInput(ConsoleKeyInfo keyInfo, FileHandler fileHandler, string currentPath)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (_selectedIndex > 0)
                {
                    _selectedIndex--;
                }
                break;
            case ConsoleKey.DownArrow:
                if (_selectedIndex < fileHandler.Files.Count - 1)
                {
                    _selectedIndex++;
                }
                break;
            case ConsoleKey.Enter:
                if (fileHandler.Files.Count > 0)
                {
                    var selectedItem = fileHandler.Files[SelectedIndex];
                    if (selectedItem.IsDirectory)
                    {
                        currentPath = selectedItem.FullPath;
                        Refresh(currentPath);
                    }
                }
                break;
            case ConsoleKey.Q:
                Core.Instance.IsRunning = false;
                break;
            case ConsoleKey.Backspace:
                DirectoryInfo parentDir = Directory.GetParent(currentPath);
                if (parentDir != null)
                {
                    currentPath = parentDir.FullName;
                    Refresh(currentPath);
                }
                break;
            case ConsoleKey.S:
                Core.Instance.CurrentMode = Mode.Search;
                InputBuffer = string.Empty;
                break;
            case ConsoleKey.R:
                if (fileHandler.Files.Count > 0 && fileHandler.Files[_selectedIndex].Name != "..")
                {
                    Core.Instance.CurrentMode = Mode.Rename;
                    InputBuffer = fileHandler.Files[_selectedIndex].Name;
                }
                break;
        }
    }

    private void HandleTextInput(ConsoleKeyInfo keyInfo, bool rename, FileHandler fileHandler, string currentPath)
    {
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            if (rename && !string.IsNullOrWhiteSpace(InputBuffer))
            {
                var selected = fileHandler.Files[_selectedIndex];
                fileHandler.RenameFile(selected, InputBuffer);
                Refresh(currentPath);
            }

            Core.Instance.CurrentMode = Mode.Browse;
            InputBuffer = string.Empty;
            return;
        }
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            Core.Instance.CurrentMode = Mode.Browse;
            InputBuffer = string.Empty;
            return;
        }
        if (keyInfo.Key == ConsoleKey.Backspace)
        {
            if (InputBuffer.Length > 0)
            {
                InputBuffer = InputBuffer[..^1];
            }
            return;
        }
        if (!char.IsControl(keyInfo.KeyChar))
        {
            InputBuffer += keyInfo.KeyChar;
        }
    }

    private void Refresh(string path)
    {
        Core.Instance.FileHandler.ClearFiles();
        Core.Instance.FileHandler.AddFiles(path);
        Core.Instance.CurrentPath = path;
        ResetSelection();
    }

    public void ResetSelection() => _selectedIndex = 0;
}
