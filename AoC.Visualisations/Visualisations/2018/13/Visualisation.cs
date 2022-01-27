using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;

namespace AoC.Visualisations.Visualisations._2018._13;

public class Visualisation : Game
{
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    public Visualisation()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 1920,
                                     PreferredBackBufferHeight = 1200
                                 };

        Content.RootDirectory = "_Content";
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Blue);

        base.Draw(gameTime);
    }
}