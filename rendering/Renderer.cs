using System;
using System.IO;
using FileHandlers;
using KeyInputs;
using CoreApp;
using Modes;

namespace Renderers;

public class Renderer
{
    public void Render(FileHandler fileHandler, KeyInput keyInput, string currentPath)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{currentPath}");

        Console.ForegroundColor = ConsoleColor.Black;
        Console.CursorVisible = false;

        for (int i = 0; i < fileHandler.Files.Count; i++)
        {
            var file = fileHandler.Files[i];
            Console.WriteLine($"{(i == keyInput.SelectedIndex ? "> " : "")} {file.Name}");
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


        Console.ForegroundColor = ConsoleColor.White;
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
        Console.BackgroundColor = ConsoleColor.DarkBlue;
    }

    public void RepairConsole()
    {
        Console.CursorVisible = true;
        Console.ResetColor();
        Console.Clear();
    }
}
