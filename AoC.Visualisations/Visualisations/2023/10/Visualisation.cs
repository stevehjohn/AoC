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

    private readonly List<Spark> _sparks = [];

    private readonly Random _rng = new();

    private char[][] _map;
    
    private PuzzleState _state;

    private int _pullSize = 12;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 980,
                                     PreferredBackBufferHeight = 980
                                 };

        Content.RootDirectory = "./10";

        IgnoreQueueLimit = true;
    }

    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            2 => new Part2(this),
            _ => throw new VisualisationParameterException()
        };
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
        for (var i = 0; i < _pullSize; i++)
        {
            if (HasNextState)
            {
                _state = GetNextState();

                if (_state.Map != null)
                {
                    _map = new char[_state.Map.Length][];

                    for (var y = 0; y < _map.Length; y++)
                    {
                        _map[y] = new char[_state.Map[y].Length];

                        Array.Copy(_state.Map[y], 0, _map[y], 0, _state.Map[y].Length);
                    }
                }
                else
                {
                    var change = _state.Change;

                    if (change.Change != '*')
                    {
                        _sparks.Add(new Spark
                        {
                            Position = new PointFloat { X = (change.X - 1) / 3f, Y = (change.Y - 1) / 3f },
                            Vector = new PointFloat { X = (-5f + _rng.Next(11)) / 10, Y = (-10f + _rng.Next(21)) / 10 },
                            Ticks = 20,
                            StartTicks = 20
                        });
                    }

                    if (change.Change == '*')
                    {
                        _pullSize = 24;
                    }

                    _map[change.Y][change.X] = change.Change;
                }
            }
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
            _spriteBatch.Draw(_spark, new Vector2(spark.Position.X * 7, spark.Position.Y * 7), new Rectangle(0, 0, 5, 5), Color.White * ((float) spark.Ticks / spark.StartTicks), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }
    
    private void DrawMap()
    {
        for (var y = 1; y < _map.Length - 1; y += 3)
        {
            for (var x = 1; x < _map[y].Length - 1; x += 3)
            {
                var tile = $"{_map[y][x]}{_map[y][x + 1]}{_map[y][x + 2]}";
                
                tile += $"{_map[y + 1][x]}{_map[y + 1][x + 1]}{_map[y + 1][x + 2]}";
                
                tile += $"{_map[y + 2][x]}{_map[y + 2][x + 1]}{_map[y + 2][x + 2]}";

                var colour = tile.Contains('X') ? Color.Red : Color.Cyan;

                var mX = (x - 1) / 3;

                var mY = (y - 1) / 3;
                
                if (tile.Contains('*'))
                {
                    for (var xx = 0; xx < 3; xx++)
                    {
                        for (var yy = 0; yy < 3; yy++)
                        {
                            if (tile[xx + yy * 3] == '*')
                            {
                                _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7 + xx * 2, mY * 7 + yy * 2),
                                    new Rectangle(49, 0, 3, 3), Color.Red, 0, Vector2.Zero, Vector2.One,
                                    SpriteEffects.None, 0);
                            }
                        }
                    }
                }
                
                if (tile.Contains('@'))
                {
                    for (var xx = 0; xx < 3; xx++)
                    {
                        for (var yy = 0; yy < 3; yy++)
                        {
                            if (tile[xx + yy * 3] == '@')
                            {
                                _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7 + xx * 2, mY * 7 + yy * 2),
                                    new Rectangle(49, 0, 3, 3), Color.Yellow, 0, Vector2.Zero, Vector2.One,
                                    SpriteEffects.None, 0);
                            }
                        }
                    }
                }

                tile = tile.Replace('#', 'X').Replace('*', '.').Replace('@', '.');

                switch (tile)
                {
                    case ".........":
                        break;
                    
                    case "...XXX...":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(0, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);

                        break;
                    case ".X..X..X.":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(7, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                    
                        break;
                    case ".X..XX...":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(14, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                    
                        break;
                    case "....XX.X.":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(21, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                    
                        break;
                    case "...XX..X.":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(28, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                    
                        break;
                    case ".X.XX....":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(35, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                    
                        break;
                    case ".X.XXX.X.":
                        _spriteBatch.Draw(_mapTiles, new Vector2(mX * 7, mY * 7), new Rectangle(42, 0, 7, 7), colour, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
                    
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
    
            spark.Vector.Y += Spark.YGravity;
        }
    
        foreach (var spark in toRemove)
        {
            _sparks.Remove(spark);
        }
    }
}