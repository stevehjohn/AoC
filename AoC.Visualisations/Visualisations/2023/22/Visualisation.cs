using AoC.Solutions.Extensions;
using AoC.Solutions.Solutions._2023._22;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2023._22;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int TileWidth = 42;

    private const int TileHeight = 38;
    
    private const int HalfTileWidth = 21;
    
    private const int TileIsoHeight = 10;
    
    private SpriteBatch _spriteBatch;
    
    private Texture2D _tile;
    
    private Texture2D _spark;

    private PuzzleState _state;

    private int[,,] _map;

    private int _yOffset;

    private readonly Queue<int> _destroy = new();

    private int? _destroying;

    private int _scrollTo;

    private readonly List<Spark> _sparks = new();

    private readonly Random _rng = new();

    private long _frame;
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 433,
            PreferredBackBufferHeight = 980
        };

        Content.RootDirectory = "./22";
    }

    public override void SetPart(int part)
    {
        switch (part)
        {
            case 1:
                Puzzle = new Part1(this);

                break;

            case 2:
                Puzzle = new Part2(this);

                break;

            default:
                throw new VisualisationParameterException();
        }
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tile = Content.Load<Texture2D>("tile");

        _spark = Content.Load<Texture2D>("spark");
    }

    protected override void Update(GameTime gameTime)
    {
        _frame++;
        
        if (HasNextState)
        {
            _state = GetNextState();

            if (_state.Settling)
            {
                _map = _state.Map;

                _yOffset += 5;
            }
            else
            {
                _destroy.Enqueue(_state.DestroyBrickId);
            }
        }

        if (_destroying != null)
        {
            _yOffset = _yOffset.Converge(_scrollTo).Converge(_scrollTo).Converge(_scrollTo).Converge(_scrollTo);

            if (_yOffset == _scrollTo)
            {
                WalkUpMap((x, y, z) =>
                {
                    if (_map[z, x, y] == _destroying.Value)
                    {
                        _map[z, x, y] = 0;

                        for (var xO = 0; xO < TileWidth; xO += 2)
                        {
                            for (var yO = 0; yO < TileHeight; yO += 2)
                            {
                                var depth = z / 1_000f + (10 - x) / 10_000f + (10 - y) / 100_000f;

                                _sparks.Add(new Spark
                                {
                                    Position = new PointFloat
                                    {
                                        X = 195 + (x - y) * HalfTileWidth + xO,
                                        Y = 960 - (TileIsoHeight * z + (x + y) * (TileIsoHeight + 4)) + yO
                                    },
                                    Vector = new PointFloat
                                        { X = (-20f + _rng.Next(41)) / 10, Y = -_rng.Next(41) / 10f },
                                    Ticks = 100,
                                    StartTicks = 100,
                                    SpriteOffset = 0,
                                    Color = GetBrickColor(_destroying.Value),
                                    Z = depth
                                });
                            }
                        }
                    }
                });
                
                _destroying = null;
            }
        }

        if (! _state.Settling && _destroying == null && _destroy.Count > 0 && _frame % 10 == 0)
        {
            _destroying = _destroy.Dequeue();
            
            WalkUpMap((x, y, z) =>
            {
                if (_map[z, x, y] == _destroying.Value)
                {
                    _scrollTo = z * 5;
                }
            });
        }
        
        UpdateSparks();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
        
        DrawBricks();
        
        DrawSparks();
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawBricks()
    {
        for (var z = 1; z < _state.Height; z++)
        {
            for (var x = 9; x >= 0; x--)
            {
                for (var y = 9; y >= 0; y--)
                {
                    var depth = z / 1_000f + (10 - x) / 10_000f + (10 - y) / 100_000f;
                        
                    var id = _map[z, x, y];

                    if (id > 0)
                    {
                        _spriteBatch.Draw(_tile, 
                            new Vector2(195 + (x - y) * HalfTileWidth, _yOffset + 960 - (TileIsoHeight * z + (x + y) * (TileIsoHeight + 4))), 
                            new Rectangle(0, 0, TileWidth, TileHeight),
                            GetBrickColor(id), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
                    }
                }
            }
        }
    }
    
    private void UpdateSparks()
    {
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
    }
    
    private void DrawSparks()
    {
        foreach (var spark in _sparks)
        {
            _spriteBatch.Draw(_spark, new Vector2(spark.Position.X, _yOffset + spark.Position.Y), new Rectangle(spark.SpriteOffset, 0, 5, 5), spark.Color * ((float) spark.Ticks / spark.StartTicks), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, spark.Z);
        }
    }

    private static Color GetBrickColor(int id)
    {
        return (id % 14) switch
        {
            1 => Color.FromNonPremultiplied(0, 0, 255, 255),
            2 => Color.FromNonPremultiplied(255, 0, 0, 255),
            3 => Color.FromNonPremultiplied(255, 0, 255, 255),
            4 => Color.FromNonPremultiplied(0, 255, 0, 255),
            5 => Color.FromNonPremultiplied(0, 255, 255, 255),
            6 => Color.FromNonPremultiplied(255, 255, 0, 255),
            7 => Color.FromNonPremultiplied(255, 255, 255, 255),
            8 => Color.FromNonPremultiplied(0, 0, 192, 255),
            9 => Color.FromNonPremultiplied(192, 0, 0, 255),
            10 => Color.FromNonPremultiplied(192, 0, 192, 255),
            11 => Color.FromNonPremultiplied(0, 192, 0, 255),
            12 => Color.FromNonPremultiplied(0, 192, 192, 255),
            13 => Color.FromNonPremultiplied(192, 192, 0, 255),
            _ => Color.FromNonPremultiplied(192, 192, 192, 255)
        };
    }

    private void WalkUpMap(Action<int, int, int> action)
    {
        for (var z = 1; z < _state.Height; z++)
        {
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    action(x, y, z);
                }
            }
        }
    }
}