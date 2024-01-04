using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Part2 = AoC.Solutions.Solutions._2019._18.Part2;
using PuzzleState = AoC.Solutions.Solutions._2019._18.PuzzleState;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2019._18;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private SpriteBatch _spriteBatch;

    private Texture2D _tiles;

    private Texture2D _sprites;

    private PuzzleState _state;

    private Texture2D _spark;

    private readonly List<Spark> _sparks = [];

    private readonly Random _rng = new();

    private readonly Color[] _colors =
    [
        Color.Blue,
        Color.Red,
        Color.Magenta,
        Color.Green,
        Color.Cyan,
        Color.Yellow,
        Color.White
    ];

    private long _frame;

    private int _color;

    private readonly Willy[] _willys = new Willy[4];

    private int _pathIndex = -1;

    private int _activeWilly;

    private int _pause;

    private readonly Queue<AoC.Solutions.Common.Point> _path = new();
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 648,
                                     PreferredBackBufferHeight = 656
                                 };

        Content.RootDirectory = "./18";
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

        _willys[0] = new Willy
        {
            MapX = 39,
            MapY = 39,
            Direction = -1,
            FrameDirection = 1,
            Cell = '1'
        };

        _willys[1] = new Willy
        {
            MapX = 41,
            MapY = 39,
            Direction = 1,
            FrameDirection = 1,
            Cell = '2'
        };

        _willys[2] = new Willy
        {
            MapX = 39,
            MapY = 41,
            Direction = -1,
            FrameDirection = 1,
            Cell = '3'
        };

        _willys[3] = new Willy
        {
            MapX = 41,
            MapY = 41,
            Direction = 1,
            FrameDirection = 1,
            Cell = '4'
        };
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tiles = Content.Load<Texture2D>("tiles");

        _sprites = Content.Load<Texture2D>("willy");

        _spark = Content.Load<Texture2D>("spark");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            _state = GetNextState();
        }

        if (_state != null)
        {
            if (_state.Path != null)
            {
                if (_pathIndex == -1)
                {
                    _pathIndex = 0;

                    StartMove();
                }
                else
                {
                    Move();
                }
            }
        }
        
        UpdateSparks();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _frame++;
        
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        DrawMap();

        DrawWillys();
        
        DrawSparks();
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    private void DrawSparks()
    {
        foreach (var spark in _sparks)
        {
            _spriteBatch.Draw(_spark, new Vector2(spark.Position.X, spark.Position.Y), new Rectangle(0, 0, 5, 5), Color.White * ((float) spark.Ticks / spark.StartTicks), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
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
    
    private void Move()
    {
        if (_pause > 0)
        {
            _pause--;
            
            return;
        }

        if (_path.Count == 0)
        {
            _pathIndex++;
            
            if (_pathIndex >= _state.Path.Length)
            {
                return;
            }

            if (char.IsNumber(_state.Path[_pathIndex]))
            {
                StartMove();
                
                return;
            }

            if (char.IsUpper(_state.Path[_pathIndex]))
            {
                return;
            }

            var cell = _willys[_activeWilly].Cell;

            if (cell > 127)
            {
                cell -= (char) 127;
            }

            var key = $"{cell}{_state.Path[_pathIndex]}";

            if (! _state.Paths.ContainsKey(key))
            {
                key = $"{_state.Path[_pathIndex]}{cell}";
            }
            
            var path = _state.Paths[key];
            
            if (path[0].X != _willys[_activeWilly].MapX || path[0].Y != _willys[_activeWilly].MapY)
            {
                path.Reverse();
            }
            
            foreach (var point in path)
            {
                _path.Enqueue(point);
            }

            return;
        }

        if (_frame % 2 == 0)
        {
            var move = _path.Dequeue();

            var wX = _willys[_activeWilly].MapX;

            if (wX > move.X)
            {
                _willys[_activeWilly].Direction = -1;
            }
            else if (wX < move.X)
            {
                _willys[_activeWilly].Direction = 1;
            }

            _willys[_activeWilly].MapX = move.X;
            _willys[_activeWilly].MapY = move.Y;
            
            var cell = _state.Map[move.X, move.Y];

            if (char.IsUpper(cell))
            {
                _willys[_activeWilly].Cell = cell;
            }

            if (char.IsLower(cell))
            {
                _willys[_activeWilly].Cell = cell;
                
                _state.Map[move.X, move.Y] = '.';
                
                cell = char.ToUpper(cell);
                
                for (var y = 0; y < _state.Map.GetLength(1); y++)
                {
                    for (var x = 0; x < _state.Map.GetLength(0); x++)
                    {
                        if (_state.Map[x, y] == cell)
                        {
                            _state.Map[x, y] += (char) 127;

                            for (var i = 0; i < 100; i++)
                            {
                                _sparks.Add(new Spark
                                {
                                    Position = new PointFloat { X = x * 8 + 4, Y = y * 8 + 4},
                                    Vector = new PointFloat { X = (-10f + _rng.Next(21)) / 10, Y = -_rng.Next(41) / 10f },
                                    Ticks = 1000,
                                    StartTicks = 1000
                                });
                            }
                        }

                        _pause = 50;
                    }
                }
            }
        }
    }

    private void StartMove()
    {
        foreach (var willy in _willys)
        {
            willy.Moving = false;
        }

        _activeWilly = _state.Path[_pathIndex] - '1';
        
        _willys[_activeWilly].Moving = true;
    }

    private void DrawWillys()
    {
        foreach (var willy in _willys)
        {
            if (willy.Moving)
            {
                if (_frame % 7 == 0)
                {
                    willy.Frame += willy.FrameDirection;

                    if (willy.Frame == 0 || willy.Frame == 2)
                    {
                        willy.FrameDirection = -willy.FrameDirection;
                    }
                }
            }

            _spriteBatch.Draw(_sprites, new Vector2(willy.MapX * 8 - 2, (willy.MapY - 1) * 8 - 1), new Rectangle(willy.Frame * 12, 0, 12, 16), Color.White, 0, Vector2.Zero, Vector2.One, willy.Direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, .1f);
        }
    }

    private void DrawMap()
    {
        if (_frame % 5 == 0)
        {
            _color++;

            if (_color == _colors.Length)
            {
                _color = 0;
            }
        }

        var keyColor = _colors[_color];

        for (var y = 0; y < _state.Map.GetLength(1); y++)
        {
            for (var x = 0; x < _state.Map.GetLength(0); x++)
            {
                var tile = _state.Map[x, y];

                if (tile == '#')
                {
                    _spriteBatch.Draw(_tiles, new Vector2(x * 8, y * 8), new Rectangle(0, 0, 8, 8), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    
                    continue;
                }

                if (char.IsLetter(tile))
                {
                    if (char.IsLower(tile))
                    {
                        _spriteBatch.Draw(_tiles, new Vector2(x * 8, y * 8), new Rectangle(16, 0, 8, 8), keyColor, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                        
                        continue;
                    }

                    if (tile < 127)
                    {
                        _spriteBatch.Draw(_tiles, new Vector2(x * 8, y * 8), new Rectangle(8, 0, 8, 8), Color.White, 0,
                            Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    }
                }
            }
        }
    }
}