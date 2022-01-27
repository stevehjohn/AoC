using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2018._13;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2018._13;

public class Visualisation : Game, IVisualiser<PuzzleState>
{
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private SpriteBatch _spriteBatch;

    private Texture2D _sprites;

    private readonly Part1 _puzzle;

    public Visualisation()
    {
        _puzzle = new Part1(this);

        _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 1200,
                                     PreferredBackBufferHeight = 1200
                                 };

        // TODO: Make a base class that does this stuff.
        // Also, something funky going on with having to add \bin\Windows - investigate.
        Content.RootDirectory = "_Content\\2020\\13\\bin\\Windows";
    }

    // TODO: Base class for easier future visualisations.
    public void PuzzleStateChanged(PuzzleState state)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        for (var y = 0; y < state.Map.GetLength(1); y++)
        {
            for (var x = 0; x < state.Map.GetLength(0); x++)
            {
                if (state.Map[x, y] != ' ')
                {
                    _spriteBatch.Draw(_sprites, new Vector2(x * 8, y * 8), new Rectangle(0, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                }
            }
        }

        _spriteBatch.End();
    }

    protected override void Draw(GameTime gameTime)
    {
        _puzzle.GetAnswer();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sprites = Content.Load<Texture2D>("sprites");
    }
}