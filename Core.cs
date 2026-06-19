using CoreApp.Files;
using CoreApp.Input;
using CoreApp.Rendering;
using CoreApp.Input;

namespace CoreApp;

public class Core
{
    private static readonly Lazy<Core> _instance = new(() => new Core());

    public static Core Instance => _instance.Value;

    public FileHandler FileHandler { get; private set; }
    public KeyInput KeyInput { get; private set; }
    public Renderer Renderer { get; private set; }

    public bool IsRunning { get; set; } = true;

    public Mode CurrentMode { get; set; } = Mode.Browse;

    public string CurrentPath { get; set; }

    public void Initialize()
    {
        Renderer = new Renderer();
        FileHandler = new FileHandler();
        KeyInput = new KeyInput();
    }
}
