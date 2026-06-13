using System;
using System.IO;

using KeyInputs;
using FileHandlers;
using CoreApp;

class Program
{
    static void Main()
    {
        Core.Instance.Initialize();

        string currentPath = Directory.GetCurrentDirectory();

        var fileHandler = Core.Instance.FileHandler;
        var keyInput = Core.Instance.KeyInput;

        fileHandler.AddFiles(currentPath);

        while (Core.Instance.IsRunning)
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($"Current directory: {currentPath}");
            Console.WriteLine("------------------------------------------------------------------");

            for (int i = 0; i < fileHandler.Files.Count; i++)
            {
                var file = fileHandler.Files[i];

                if (i == keyInput.SelectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"> {file.Name}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{file.Name}");
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            var action = keyInput.HandleKeyInput(keyInfo, fileHandler.Files.Count);

            if (action == KeyInput.Action.Enter && fileHandler.Files.Count > 0)
            {
                var selectedItem = fileHandler.Files[keyInput.SelectedIndex];
                if (selectedItem is DirectoryInfo)
                {
                    currentPath = selectedItem.FullName;
                    fileHandler.ClearFiles();
                    fileHandler.AddFiles(currentPath);
                    keyInput.ResetSelection();
                }
            }
            else if (action == KeyInput.Action.Back)
            {
                DirectoryInfo parentDir = Directory.GetParent(currentPath);
                if (parentDir != null)
                {
                    currentPath = parentDir.FullName;
                    fileHandler.ClearFiles();
                    fileHandler.AddFiles(currentPath);
                    keyInput.ResetSelection();
                }
            }
        }
        Console.Clear();
    }
}
