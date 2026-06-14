using System;
using System.IO;

using KeyInputs;
using FileHandlers;
using CoreApp;
using Renderers;

class Program
{
    static void Main()
    {
        Core.Instance.Initialize();

        string currentPath = Directory.GetCurrentDirectory();

        var fileHandler = Core.Instance.FileHandler;
        var keyInput = Core.Instance.KeyInput;
        var renderer = Core.Instance.Renderer;

        fileHandler.AddFiles(currentPath);

        while (Core.Instance.IsRunning)
        {
            renderer.Render(fileHandler, keyInput, currentPath);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            var action = keyInput.HandleKeyInput(keyInfo, fileHandler.Files.Count);

            bool pathChanged = false;
            if (action == KeyInput.Action.Enter && fileHandler.Files.Count > 0)
            {
                var selectedItem = fileHandler.Files[keyInput.SelectedIndex];
                if (selectedItem.IsDirectory)
                {
                    currentPath = selectedItem.FullPath;
                    pathChanged = true;
                }
            }
            else if (action == KeyInput.Action.Back)
            {
                DirectoryInfo parentDir = Directory.GetParent(currentPath);
                if (parentDir != null)
                {
                    currentPath = parentDir.FullName;
                    pathChanged = true;
                }
            }

            if (pathChanged)
            {
                fileHandler.ClearFiles();
                fileHandler.AddFiles(currentPath);
                keyInput.ResetSelection();
            }
        }
        renderer.RepairConsole();
    }
}
