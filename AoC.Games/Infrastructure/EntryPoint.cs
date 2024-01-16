using Microsoft.Xna.Framework;

namespace AoC.Games.Infrastructure;

public static class EntryPoint
{
    [STAThread]
    private static void Main(string[] arguments)
    {
        var gameName = arguments.Length > 0 ? arguments[0] : string.Empty;

        Game game;        
        
        switch (gameName)
        {
            case "mirrors":
                game = new Games.Deflectors.Game();
                break;
            
            default:
                game = new Games.Mazes.Game();
                
                break;
        }
        
        game.Run();
    }
}