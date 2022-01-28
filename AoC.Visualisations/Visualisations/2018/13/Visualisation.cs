using System.Diagnostics;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2018._13;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2018._13;

public class Visualisation : Game, IVisualiser<PuzzleState>
{
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private SpriteBatch _spriteBatch;

    private Texture2D _mapTiles;

    private Texture2D _spark;

    private readonly Part1 _puzzle;

    private readonly List<Spark> _sparks = new();

    private readonly Random _rng = new();

    public Visualisation()
    {
        _puzzle = new Part1(this);

        _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 1050,
                                     PreferredBackBufferHeight = 1050
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

        DrawMap(state);

        DrawCarts(state);

        _spriteBatch.End();
    }

    private void DrawCarts(PuzzleState state)
    {
        foreach (var cart in state.Carts)
        {
            if (_rng.Next(100) == 0)
            {
                _sparks.Add(new Spark { Position = new PointFloat { X = cart.X, Y = cart.Y }, Vector = new PointFloat { X = (-5f + _rng.Next(11)) / 10, Y = (-10f + _rng.Next(21)) / 10 } });
            }
        }

        var toRemove = new List<Spark>();

        foreach (var spark in _sparks)
        {
            _spriteBatch.Draw(_spark, new Vector2(spark.Position.X * 7, spark.Position.Y * 7), new Rectangle(0, 0, 5, 5), Color.White * (1 - spark.Ticks / 20f), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);

            spark.Ticks++;

            if (spark.Ticks > 20)
            {
                toRemove.Add(spark);
            }

            spark.Position.X += spark.Vector.X;

            spark.Position.Y += spark.Vector.Y;

            spark.Vector.Y += 0.1f;
        }

        foreach (var spark in toRemove)
        {
            _sparks.Remove(spark);
        }

        Debug.WriteLine(_sparks.Count);
    }

    private void DrawMap(PuzzleState state)
    {
        for (var y = 0; y < state.Map.GetLength(1); y++)
        {
            for (var x = 0; x < state.Map.GetLength(0); x++)
            {
                switch (state.Map[x, y])
                {
                    case '─':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7, y * 7), new Rectangle(0, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '│':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7, y * 7), new Rectangle(7, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '└':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7, y * 7), new Rectangle(14, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '┌':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7, y * 7), new Rectangle(21, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '┐':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7, y * 7), new Rectangle(28, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '┘':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7, y * 7), new Rectangle(35, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '┼':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7, y * 7), new Rectangle(42, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                }
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        _puzzle.GetAnswer();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _mapTiles = Content.Load<Texture2D>("map-tiles");

        _spark = Content.Load<Texture2D>("spark");
    }
}

public class Spark
{
    public PointFloat Position { get; set; }

    public PointFloat Vector { get; set; }

    public int Ticks { get; set; }
}

public class PointFloat
{
    public float X { get; set; }

    public float Y { get; set; }
}