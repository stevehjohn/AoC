using AoC.Solutions.Solutions._2019._18;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AoCPoint = AoC.Solutions.Common.Point;
using Color = Microsoft.Xna.Framework.Color;
using Part2 = AoC.Solutions.Solutions._2019._18.Part2;
using PuzzleState = AoC.Solutions.Solutions._2019._18.PuzzleState;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2019._18;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
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

    private readonly char[] _targets = new char[4];

    private Queue<AoCPoint>[] _paths;

    private SpriteBatch _spriteBatch;

    private Texture2D _tiles;

    private Texture2D _sprites;

    private PuzzleState _state;

    private Texture2D _spark;

    private long _frame;

    private int _color;

    private Willy[] _willys;

    private int _pathIndex = -1;

    private int _activeWilly;

    private int _pause;

    private int _willyCount;

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
        Puzzle = part switch
        {
            1 => new Part1(this),
            2 => new Part2(this),
            _ => throw new VisualisationParameterException()
        };

        _willyCount = part switch
        {
            1 => 1,
            2 => 4,
            _ => throw new VisualisationParameterException()
        };

        _willys = new Willy[_willyCount];

        _paths = new Queue<AoCPoint>[_willyCount];

        for (var i = 0; i < _willyCount; i++)
        {
            _paths[i] = new Queue<AoCPoint>();
        }
    }

    protected override void Initialize()
    {
        IsMouseVisible = true;

        if (_willyCount == 1)
        {
            _willys[0] = new Willy
            {
                MapX = 40,
                MapY = 40,
                Direction = -1,
                FrameDirection = 1,
                Cell = '@'
            };
        }
        else
        {
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
        }

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

        if (_state is { Path: not null })
        {
            if (_pathIndex == -1)
            {
                _activeWilly = 0;

                AdvanceSolution();
            }
            else
            {
                Move();
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

            spark.Vector.Y += Spark.YGravity;
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

        if (_paths[_activeWilly].Count == 0)
        {
            _targets[_activeWilly] = '\0';

            AdvanceSolution();

            return;
        }

        if (_frame % 2 != 0)
        {
            return;
        }

        foreach (var willy in _willys)
        {
            willy.Moving = false;
        }

        for (var i = 0; i < _willys.Length; i++)
        {
            MoveWilly(i);
        }
    }

    private void MoveWilly(int index)
    {
        var path = _paths[index];

        if (path.Count == 0)
        {
            return;
        }

        var move = path.Peek();

        var tile = _state.Map[move.X, move.Y];

        if (index != _activeWilly && (char.IsLower(tile) || IsLockedDoor(tile)))
        {
            return;
        }

        path.Dequeue();

        var willy = _willys[index];

        if (willy.MapX > move.X)
        {
            willy.Direction = -1;
        }
        else if (willy.MapX < move.X)
        {
            willy.Direction = 1;
        }

        willy.MapX = move.X;

        willy.MapY = move.Y;

        willy.Moving = true;

        if (! char.IsLower(tile))
        {
            return;
        }

        willy.Cell = tile;

        _state.Map[move.X, move.Y] = '.';

        OpenDoor(char.ToUpper(tile));
    }

    private static bool IsLockedDoor(char tile)
    {
        return tile < 127 && char.IsUpper(tile);
    }

    private void OpenDoor(char door)
    {
        for (var y = 0; y < _state.Map.GetLength(1); y++)
        {
            for (var x = 0; x < _state.Map.GetLength(0); x++)
            {
                if (_state.Map[x, y] != door)
                {
                    continue;
                }

                _state.Map[x, y] += (char) 127;

                for (var i = 0; i < 100; i++)
                {
                    _sparks.Add(new Spark
                    {
                        Position = new PointFloat { X = x * 8 + 4, Y = y * 8 + 4 },
                        Vector = new PointFloat { X = (-10f + _rng.Next(21)) / 10, Y = -_rng.Next(41) / 10f },
                        Ticks = 1000,
                        StartTicks = 1000
                    });
                }
            }
        }
    }

    private void AdvanceSolution()
    {
        while (true)
        {
            _pathIndex++;

            if (_pathIndex >= _state.Path.Length)
            {
                _pathIndex = _state.Path.Length - 1;
                return;
            }

            var token = _state.Path[_pathIndex];

            if (IsRobotMarker(token))
            {
                StartMove();

                continue;
            }

            if (char.IsUpper(token))
            {
                return;
            }

            if (! char.IsLower(token))
            {
                continue;
            }

            if (_targets[_activeWilly] != token || _paths[_activeWilly].Count == 0)
            {
                _paths[_activeWilly].Clear();

                QueueRoute(_activeWilly, token);
            }

            _targets[_activeWilly] = token;

            if (_willyCount > 1)
            {
                PlanAhead();
            }

            return;
        }
    }

    private bool IsRobotMarker(char token)
    {
        return _willyCount > 1 && token is >= '1' and <= '4';
    }

    private void PlanAhead()
    {
        var robot = _activeWilly;

        for (var i = _pathIndex + 1; i < _state.Path.Length; i++)
        {
            var token = _state.Path[i];

            if (IsRobotMarker(token))
            {
                robot = token - '1';

                continue;
            }

            if (! char.IsLower(token) || robot == _activeWilly)
            {
                continue;
            }

            if (_targets[robot] == '\0' && _paths[robot].Count == 0)
            {
                QueueRoute(robot, token);
                
                _targets[robot] = token;
            }
        }
    }

    private void QueueRoute(int robot, char target)
    {
        var cell = NormaliseCell(_willys[robot].Cell);

        var key = $"{cell}{target}";

        if (! _state.Paths.TryGetValue(key, out var route))
        {
            key = $"{target}{cell}";

            if (! _state.Paths.TryGetValue(key, out route))
            {
                return;
            }
        }

        var willy = _willys[robot];

        var distanceToStart = Math.Abs(route[0].X - willy.MapX) + Math.Abs(route[0].Y - willy.MapY);

        var distanceToEnd = Math.Abs(route[^1].X - willy.MapX) + Math.Abs(route[^1].Y - willy.MapY);

        var reversed = distanceToStart != 0 && (distanceToEnd == 0 || distanceToEnd < distanceToStart);

        if (! reversed)
        {
            foreach (var point in route) EnqueueIfNew(robot, point);
        }
        else
        {
            for (var i = route.Count - 1; i >= 0; i--) EnqueueIfNew(robot, route[i]);
        }
    }

    private void EnqueueIfNew(int robot, AoCPoint point)
    {
        var willy = _willys[robot];

        if (point.X == willy.MapX && point.Y == willy.MapY)
        {
            return;
        }

        _paths[robot].Enqueue(point);
    }

    private static char NormaliseCell(char cell)
    {
        return cell > 127 ? (char) (cell - 127) : cell;
    }

    private void StartMove()
    {
        _activeWilly = _state.Path[_pathIndex] - '1';
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

                    if (willy.Frame is 0 or 2)
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
                        _spriteBatch.Draw(_tiles, new Vector2(x * 8, y * 8), new Rectangle(8, 0, 8, 8), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    }
                }
            }
        }
    }
}