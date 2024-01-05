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

    private float _yOffset;

    private readonly Queue<int> _destroyQueue = new();

    private readonly List<Spark> _sparks = [];

    private readonly Random _rng = new();

    private long _frame;

    private int _phase;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 513,
            PreferredBackBufferHeight = 980
        };

        Content.RootDirectory = "./22";
    }

    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            1 => new Part1(this),
            2 => new Part2(this),
            _ => throw new VisualisationParameterException()
        };
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tile = Content.Load<Texture2D>("tile");

        _spark = Content.Load<Texture2D>("spark");
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            switch (_phase)
            {
                case 0:
                    _state = GetNextState();
                    
                    _map = _state.Map;

                    _phase = 1;

                    break;
                
                case 1:
                    if (PeekNextState().Settling)
                    {
                        if (_frame % 2 == 0)
                        {
                            _state = GetNextState();

                            _map = _state.Map;
                        }

                        _yOffset += 2.5f;
                    }
                    else
                    {
                        _phase = 2;
                    }
                    
                    break;
                
                case 2:
                    _yOffset -= 2.5f;

                    if (_yOffset <= 0)
                    {
                        _phase = 3;
                    }
                    
                    break;

                case 3:
                    if (PeekNextState().Settling)
                    {
                        if (_destroyQueue.Count == 0)
                        {
                            _phase = 4;
                        }

                        break;
                    }

                    _state = GetNextState();
                    
                    _destroyQueue.Enqueue(_state.DestroyBrickId);

                    break;
                
                case 4:
                    _yOffset -= 2.5f;

                    if (_yOffset <= 0)
                    {
                        _phase = 5;
                    }
                    
                    break;
                
                case 5:
                    if (_frame % 2 == 0)
                    {
                        _state = GetNextState();

                        _map = _state.Map;
                    }

                    break;
            }
        }

        if (_phase == 3 && _destroyQueue.Count > 0)
        {
            _yOffset += 0.2f;
        }

        if (_frame % (Puzzle is Part1 ? 10 : 5) == 0 && _destroyQueue.TryDequeue(out var brickId))
        {
            WalkUpMap((x, y, z) =>
            {
                if (_map[z, x, y] == brickId)
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
                                    X = 235 + (x - y) * HalfTileWidth + xO,
                                    Y = 960 - (TileIsoHeight * z + (x + y) * (TileIsoHeight + 4)) + yO
                                },
                                Vector = new PointFloat
                                    { X = (-20f + _rng.Next(41)) / 10, Y = -_rng.Next(41) / 10f },
                                Ticks = 100,
                                StartTicks = 100,
                                SpriteOffset = 0,
                                Color = GetBrickColor(brickId),
                                Z = depth
                            });
                        }
                    }
                }
            });
        }

        UpdateSparks();

        _frame++;

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
                            new Vector2(235 + (x - y) * HalfTileWidth,
                                _yOffset + 960 - (TileIsoHeight * z + (x + y) * (TileIsoHeight + 4))),
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
            _spriteBatch.Draw(_spark, new Vector2(spark.Position.X, _yOffset + spark.Position.Y),
                new Rectangle(spark.SpriteOffset, 0, 5, 5), spark.Color * ((float) spark.Ticks / spark.StartTicks), 0,
                Vector2.Zero, Vector2.One, SpriteEffects.None, spark.Z);
        }
    }

    private static Color GetBrickColor(int id)
    {
        const int opacity = 208;
        
        return (id % 14) switch
        {
            1 => Color.FromNonPremultiplied(0, 0, 255, opacity),
            2 => Color.FromNonPremultiplied(255, 0, 0, opacity),
            3 => Color.FromNonPremultiplied(255, 0, 255, opacity),
            4 => Color.FromNonPremultiplied(0, 255, 0, opacity),
            5 => Color.FromNonPremultiplied(0, 255, 255, opacity),
            6 => Color.FromNonPremultiplied(255, 255, 0, opacity),
            7 => Color.FromNonPremultiplied(255, 255, 255, opacity),
            8 => Color.FromNonPremultiplied(0, 0, 192, opacity),
            9 => Color.FromNonPremultiplied(192, 0, 0, opacity),
            10 => Color.FromNonPremultiplied(192, 0, 192, opacity),
            11 => Color.FromNonPremultiplied(0, 192, 0, opacity),
            12 => Color.FromNonPremultiplied(0, 192, 192, opacity),
            13 => Color.FromNonPremultiplied(192, 192, 0, opacity),
            _ => Color.FromNonPremultiplied(192, 192, 192, opacity)
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