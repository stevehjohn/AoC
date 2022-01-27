using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2018._13;

public class Visualisation : Game
{
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private SpriteBatch _spriteBatch;

    private Texture2D _sprites;

    public Visualisation()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 1200,
                                     PreferredBackBufferHeight = 1200
                                 };

        // TODO: Make a base class that does this stuff.#
        // Also, something funky going on with having to add \bin\Windows - investigate.
        Content.RootDirectory = "_Content\\2020\\13\\bin\\Windows";
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        _spriteBatch.Draw(_sprites, new Vector2(100, 100), new Rectangle(0, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sprites = Content.Load<Texture2D>("sprites");
    }
}