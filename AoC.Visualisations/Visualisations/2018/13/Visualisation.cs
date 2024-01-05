using AoC.Solutions.Solutions._2018._13;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = AoC.Solutions.Common.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2018._13;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private SpriteBatch _spriteBatch;

    private Texture2D _mapTiles;

    private Texture2D _spark;

    private readonly List<Spark> _sparks = [];

    private readonly Random _rng = new();

    private PuzzleState _state;

    private Dictionary<int, Point> _carts;

    private Dictionary<int, Point> _nextCarts;

    private readonly List<Collision> _collisions = [];

    private bool _fast;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 1150,
                                     PreferredBackBufferHeight = 1150
                                 };

        Content.RootDirectory = "./13";
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

                _fast = true;

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
            if (_carts == null || _carts.Count > 0)
            {
                if (! UpdateCarts())
                {
                    _carts = _nextCarts;

                    _state = GetNextState();

                    _nextCarts = GetTranslatedCarts();

                    if (_carts == null)
                    {
                        _carts = _nextCarts;

                        _state = GetNextState();

                        _nextCarts = GetTranslatedCarts();
                    }

                    UpdateCarts();
                }
            }
        }
        else if (_nextCarts != null)
        {
            _carts = _nextCarts;
        }

        // In an ideal world, these should work in either order, but they don't. As it works, going to leave for now.
        UpdateSparks();

        UpdateCollisions();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (_state != null)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            DrawMap();

            DrawCarts();

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
            _spriteBatch.Draw(_spark, new Vector2(spark.Position.X, spark.Position.Y), new Rectangle(spark.SpriteOffset, 0, 5, 5), Color.White * ((float) spark.Ticks / spark.StartTicks), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }

    private void DrawCarts()
    {
        foreach (var cart in _carts)
        {
            _spriteBatch.Draw(_spark, new Vector2(cart.Value.X, cart.Value.Y), new Rectangle(0, 0, 5, 5), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }

    private void DrawMap()
    {
        for (var y = 0; y < _state.Map.GetLength(1); y++)
        {
            for (var x = 0; x < _state.Map.GetLength(0); x++)
            {
                switch (_state.Map[x, y])
                {
                    case '─':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7 + 50, y * 7 + 50), new Rectangle(0, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '│':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7 + 50, y * 7 + 50), new Rectangle(7, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '└':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7 + 50, y * 7 + 50), new Rectangle(14, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '┌':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7 + 50, y * 7 + 50), new Rectangle(21, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '┐':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7 + 50, y * 7 + 50), new Rectangle(28, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '┘':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7 + 50, y * 7 + 50), new Rectangle(35, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                    case '┼':
                        _spriteBatch.Draw(_mapTiles, new Vector2(x * 7 + 50, y * 7 + 50), new Rectangle(42, 0, 7, 7), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

                        break;
                }
            }
        }
    }

    private bool UpdateCarts()
    {
        if (_carts == null || _carts.Count == 0)
        {
            return false;
        }

        var moved = false;

        foreach (var cart in _carts)
        {
            if (! _nextCarts.ContainsKey(cart.Key))
            {
                continue;
            }

            var target = _nextCarts[cart.Key];

            if (_fast)
            {
                cart.Value.X = target.X;

                cart.Value.Y = target.Y;
            }
            else
            {
                if (! cart.Value.Equals(target))
                {
                    cart.Value.X += Math.Sign(target.X - cart.Value.X);

                    cart.Value.Y += Math.Sign(target.Y - cart.Value.Y);

                    moved = true;
                }
            }
        }

        return moved;
    }
    
    private void UpdateCollisions()
    {
        if (_state.CollisionPoint != null)
        {
            if (_state.IsFinalState)
            {
                _collisions.Add(new Collision { Position = _state.Carts.Single().Position, Ticks = int.MaxValue, SpriteOffset = 10, IsFinal = true });

                _collisions.Add(new Collision { Position = _state.CollisionPoint, Ticks = 200, SpriteOffset = 5 });

                _state.CollisionPoint = null;
            }
            else
            {
                if (Puzzle is Part1)
                {
                    _collisions.Add(new Collision { Position = _state.CollisionPoint, Ticks = int.MaxValue, SpriteOffset = 5 });

                    _state.CollisionPoint = null;
                }
                else
                {
                    _collisions.Add(new Collision { Position = _state.CollisionPoint, Ticks = 200, SpriteOffset = 5 });
                }
            }
        }

        var toRemove = new List<Collision>();

        foreach (var collision in _collisions)
        {
            for (var i = 0; i < 4; i++)
            {
                _sparks.Add(new Spark
                            {
                                SpriteOffset = collision.SpriteOffset,
                                Position = new PointFloat { X = collision.Position.X * 7 + 50, Y = collision.Position.Y * 7 + 50 },
                                Vector = new PointFloat { X = (-10f + _rng.Next(21)) / 10, Y = -_rng.Next(41) / 10f },
                                Ticks = 120,
                                StartTicks = 120,
                                YGravity = collision.IsFinal ? -0.1f : 0.025f
                            });
            }

            collision.Ticks--;

            if (collision.Ticks < 0)
            {
                toRemove.Add(collision);
            }
        }

        foreach (var collision in toRemove)
        {
            _collisions.Remove(collision);
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

        foreach (var cart in _carts)
        {
            if (_state.CollisionPoint != null)
            {
                if (_carts.Count(c => c.Value.X == cart.Value.X && c.Value.Y == cart.Value.Y) > 1)
                {
                    continue;
                }
            }

            if (_rng.Next(2) == 0)
            {
                _sparks.Add(new Spark
                            {
                                Position = new PointFloat { X = cart.Value.X, Y = cart.Value.Y },
                                Vector = new PointFloat { X = (-5f + _rng.Next(11)) / 10, Y = (-10f + _rng.Next(21)) / 10 },
                                Ticks = 20,
                                StartTicks = 20
                            });
            }
        }
    }

    private Dictionary<int, Point> GetTranslatedCarts()
    {
        return _state.Carts.ToDictionary(c => c.Id, c => new Point(c.Position.X * 7 + 51, c.Position.Y * 7 + 51));
    }
}