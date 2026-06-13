using FileHandlers;
using KeyInputs;

namespace CoreApp;

public class Core
{
    private static readonly Lazy<Core> _instance = new(() => new Core());

    public static Core Instance => _instance.Value;

    public FileHandler FileHandler { get; private set; }
    public KeyInput KeyInput { get; private set; }

    public bool IsRunning { get; set; } = true;

    public void Initialize()
    {
        FileHandler = new FileHandler();
        KeyInput = new KeyInput();
    }
}
