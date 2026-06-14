using CoreApp;
using System;

using FileHandlers;

namespace KeyInputs;

public class KeyInput
{
    private int _selectedIndex = 0;

    public int SelectedIndex => _selectedIndex;

    public enum Action
    {
        None, Up, Down, Enter, Back, Quit
    }

    public Action HandleKeyInput(ConsoleKeyInfo keyInfo, FileHandler fileHandler, string currentPath)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                MoveUp();
                return Action.Up;
            case ConsoleKey.DownArrow:
                MoveDown(fileHandler.Files.Count);
                return Action.Down;
            case ConsoleKey.Enter:
                if (fileHandler.Files.Count > 0)
                {
                    var selectedItem = fileHandler.Files[SelectedIndex];
                    if (selectedItem.IsDirectory)
                    {
                        currentPath = selectedItem.FullPath;
                        fileHandler.ClearFiles();
                        fileHandler.AddFiles(currentPath);
                        ResetSelection();
                    }
                }
                return Action.Enter;
            case ConsoleKey.Q:
                Core.Instance.IsRunning = false;
                return Action.Quit;
            case ConsoleKey.Backspace:
                DirectoryInfo parentDir = Directory.GetParent(currentPath);
                if (parentDir != null)
                {
                    currentPath = parentDir.FullName;
                    fileHandler.ClearFiles();
                    fileHandler.AddFiles(currentPath);
                    ResetSelection();
                }
                return Action.Back;
            default:
                return Action.None;
        }
    }

    private void MoveUp()
    {
        if (_selectedIndex > 0)
        {
            _selectedIndex--;
        }
    }

    private void MoveDown(int count)
    {
        if (_selectedIndex < count - 1)
        {
            _selectedIndex++;
        }
    }

    public void ResetSelection()
    {
        _selectedIndex = 0;
    }
}
