using AoC.Games.Games.Deflectors;

namespace AoC.Games.Infrastructure;

public static class EntryPoint
{
    [STAThread]
    private static void Main()
    {
        var game = new Game();
        
        game.Run();
    }
}