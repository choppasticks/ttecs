using CoreApp;
using System.IO;
using TerminalStates;

class Program
{
    static void Main()
    {
        Core.Instance.Initialize();

        Core.Instance.CurrentPath = Directory.GetCurrentDirectory();
        var fileHandler = Core.Instance.FileHandler;
        var keyInput = Core.Instance.KeyInput;
        var renderer = Core.Instance.Renderer;

        var terminalState = new TerminalState();

        fileHandler.AddFiles(Core.Instance.CurrentPath);

        try
        {
            terminalState.Save();
            while (Core.Instance.IsRunning)
            {
                renderer.Render(fileHandler, keyInput, Core.Instance.CurrentPath);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyInput.HandleKeyInput(keyInfo, fileHandler, Core.Instance.CurrentPath);
            }
        } catch (Exception e)
        {
            terminalState.Restore();
            Console.WriteLine(e.StackTrace);
            Environment.Exit(1);
        } finally
        {
            terminalState.Restore();
        }
    }
}
