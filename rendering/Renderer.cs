using System;
using System.IO;
using FileHandlers;
using KeyInputs;
using CoreApp;
using Modes;

namespace Renderers;

public class Renderer
{
    public void Render(FileHandler fileHandler, KeyInput keyInput)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{Directory.GetCurrentDirectory()}");
        Console.CursorVisible = false;

        for (int i = 0; i < fileHandler.Files.Count; i++)
        {
            var file = fileHandler.Files[i];

            string start = file.IsDirectory ? "📁" : "📄";

            Console.WriteLine($"{start}{(i == keyInput.SelectedIndex ? "> " : "")} {file.Name}");
        }

        var currentFile = fileHandler.Files[keyInput.SelectedIndex];

        if (File.Exists(currentFile.FullPath))
        {
            string preview = fileHandler.FilePreview.PreviewFile(currentFile.FullPath, 25, 45);
            string[] previewText = preview.Split('\n');

            for (int i = 0; i < previewText.Length; i++)
            {
                Console.SetCursorPosition(60, 3 + i);
                Console.Write(previewText[i].PadRight(40));
            }
        }

        Console.ForegroundColor = ConsoleColor.White;
        RenderFooter();
    }

    private void RenderFooter()
    {
        int bottomRow = Console.WindowHeight - 1;
        Console.SetCursorPosition(0, bottomRow);
        Console.Write(new string(' ', Console.WindowWidth));

        Console.SetCursorPosition(0, bottomRow);

        switch (Core.Instance.CurrentMode)
        {
            case Mode.Browse:
                Console.Write("[BROWSE] ");
                Console.Write("Arrows: Nav | Enter: Open | S: Search | R: Rename | Q: Quit");
                break;
            case Mode.Search:
                Console.Write("[SEARCH] Type: ");
                Console.Write($"{Core.Instance.KeyInput.InputBuffer}_  (Press Enter to finish, ESC to abort)");
                break;
            case Mode.Rename:
                Console.Write("[RENAME] New Name: ");
                Console.Write($"{Core.Instance.KeyInput.InputBuffer}_  (Press Enter to apply, ESC to abort)");
                break;
        }
    }

    public void RepairConsole()
    {
        Console.CursorVisible = true;
        Console.ResetColor();
        Console.Clear();
    }
}
