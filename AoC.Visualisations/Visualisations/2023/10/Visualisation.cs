using System.Diagnostics.CodeAnalysis;
using AoC.Solutions.Solutions._2023._10;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2023._10;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private SpriteBatch _spriteBatch;

    private Texture2D _mapTiles;

    private Texture2D _spark;

    private readonly List<Spark> _sparks = new();

    private readonly Random _rng = new();

    private PuzzleState _state;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 980,
                                     PreferredBackBufferHeight = 980
                                 };

        // Something funky going on with having to add \bin\Windows - investigate.
        Content.RootDirectory = "./10";
    }

    public override void SetPart(int part)
    {
        switch (part)
        {
            case 2:
                Puzzle = new Part2(this);

                break;
            default:
                throw new VisualisationParameterException();
        }
    }

    protected override void Initialize()
    {
        IsMouseVisible = true;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _mapTiles = Content.Load<Texture2D>("map-tiles");

        _spark = Content.Load<Texture2D>("spark");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            _state = GetNextState();
        }

        UpdateSparks();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (_state != null)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            DrawMap();

            DrawSparks();
            
            _spriteBatch.End();
        }
        else
        {
            GraphicsDevice.Clear(Color.Black);
        }

        base.Draw(gameTime);
    }
    
    private void DrawSparks()
    {
        foreach (var spark in _sparks)
        {
            _spriteBatch.Draw(_spark, new Vector2(spark.Position.X * 7, spark.Position.Y * 7), new Rectangle(spark.SpriteOffset, 0, 5, 5), Color.White * ((float) spark.Ticks / spark.StartTicks), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }
    
    [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
    private void DrawMap()
    {
        for (var y = 1; y < _state.Map.Length - 1; y += 3)
        {
            for (var x = 1; x < _state.Map[y].Length - 1; x += 3)
            {
                var tile = $"{_state.Map[y][x]}{_state.Map[y][x + 1]}{_state.Map[y][x + 2]}";
                
                tile += $"{_state.Map[y + 1][x]}{_state.Map[y + 1][x + 1]}{_state.Map[y + 1][x + 2]}";
                
                tile += $"{_state.Map[y + 2][x]}{_state.Map[y + 2][x + 1]}{_state.Map[y + 2][x + 2]}";

                var colour = tile.Contains("X") ? Color.Cyan : Color.Red;
                    
                tile = tile.Replace('#', 'X');

                var mX = (x - 1) / 3;

                var mY = (y - 1) / 3;
                
                switch (tile)
                {
                    case "...XXX...":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(0, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case ".X..X..X.":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(7, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    
                        break;
                    case ".X..XX...":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(14, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    
                        break;
                    case "....XX.X.":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(21, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    
                        break;
                    case "...XX..X.":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(28, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    
                        break;
                    case ".X.XX....":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(35, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    
                        break;
                    case ".X.XXX.X.":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(42, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    
                        break;
                }
            }
        }
    }
    
    private void UpdateSparks()
    {
        if (_state == null)
        {
            return;
        }

        var toRemove = new List<Spark>();
    
        foreach (var spark in _sparks)
        {
            spark.Ticks--;
    
            if (spark.Ticks < 0)
            {
                toRemove.Add(spark);
    
                continue;
            }
    
            spark.Position.X += spark.Vector.X;
    
            spark.Position.Y += spark.Vector.Y;
    
            spark.Vector.Y += spark.YGravity;
        }
    
        foreach (var spark in toRemove)
        {
            _sparks.Remove(spark);
        }

        for (var i = 0; i < 6; i++)
        {
            if (_state.Changes.Count > 0)
            {
                if (_state.Changes.TryDequeue(out var change))
                {
                    _sparks.Add(new Spark
                    {
                        Position = new PointFloat { X = (change.X - 1) / 3f, Y = (change.Y - 1) / 3f },
                        Vector = new PointFloat { X = (-5f + _rng.Next(11)) / 10, Y = (-10f + _rng.Next(21)) / 10 },
                        Ticks = 20,
                        StartTicks = 20
                    });

                    _state.Map[change.Y][change.X] = change.Change;
                }
            }
        }
    }
}