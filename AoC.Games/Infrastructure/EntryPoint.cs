using System;
using AoC.Games.Games.Deflectors;

namespace AoC.Games.Infrastructure;

public static class EntryPoint
{
    [STAThread]
    private static void Main(string[] arguments)
    {
        var game = new Game();
        
        game.Run();
    }
}