using AoC.Games.Games.Deflectors;
using AoC.Games.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace AoC.Games.Games.Mazes;

[UsedImplicitly]
public class Game : Microsoft.Xna.Framework.Game
{
    private const int Width = 50;

    private const int Height = 50;

    public readonly bool[,] _maze = new bool[Width, Height];
    
    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;

    public Game()
    {
        var scaleFactor = AppSettings.Instance.ScaleFactor;

        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = (int) (Constants.BufferWidth * scaleFactor),
            PreferredBackBufferHeight = (int) (Constants.BufferHeight * scaleFactor)
        };

        Content.RootDirectory = "./Deflectors";
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}