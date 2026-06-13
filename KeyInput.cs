using CoreApp;
using System;

namespace KeyInputs;

public class KeyInput
{
    private int _selectedIndex = 0;

    public int SelectedIndex => _selectedIndex;

    public enum Action
    {
        None, Up, Down, Enter, Back, Quit
    }

    public Action HandleKeyInput(ConsoleKeyInfo keyInfo, int count)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                MoveUp();
                return Action.Up;
            case ConsoleKey.DownArrow:
                MoveDown(count);
                return Action.Down;
            case ConsoleKey.Enter:
                return Action.Enter;
            case ConsoleKey.Q:
                Core.Instance.IsRunning = false;
                return Action.Quit;
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
