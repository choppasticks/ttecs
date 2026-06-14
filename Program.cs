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
            keyInput.HandleKeyInput(keyInfo, fileHandler, currentPath);
        }
        renderer.RepairConsole();
    }
}
