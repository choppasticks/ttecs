using System;

namespace CoreApp.TerminalStates;

public class TerminalState
{
	private const string Esc = "\x1b";

	public void Save()
	{
        Console.Write($"{Esc}[?1049h");
        Console.Write($"{Esc}[?25l");
        Console.Write($"{Esc}[H{Esc}[2J");
	}

	public void Restore()
	{
  		Console.Write($"{Esc}[?25h");
        Console.Write($"{Esc}[0m");
        Console.Write($"{Esc}[?1049l");
	}
}