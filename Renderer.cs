using System;
using System.IO;
using FileHandlers;
using KeyInputs;

namespace Renderers;

public class Renderer
{
    public void Render(FileHandler fileHandler, KeyInput keyInput, string currentPath)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear();
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine($"Current directory: {currentPath}");
        Console.WriteLine("------------------------------------------------------------------");

        Console.CursorVisible = false;

        for (int i = 0; i < fileHandler.Files.Count; i++)
        {
            var file = fileHandler.Files[i];

            Console.WriteLine($"{(i == keyInput.SelectedIndex ? ">" : "")} {file.Name}");
        }

        RenderFooter("Q-Quit; Backspace-Back; Enter-Enter Dir; Up; Down");
    }

    private void RenderFooter(string text)
    {
        int bottomRow = Console.WindowHeight - 1;
        Console.SetCursorPosition(0, bottomRow);
        Console.Write(new string(' ', Console.WindowWidth));

        Console.SetCursorPosition(0, bottomRow);
        Console.Write(text);
    }

    public void RepairConsole()
    {
        Console.CursorVisible = true;
        Console.ResetColor();
        Console.Clear();
    }
}
