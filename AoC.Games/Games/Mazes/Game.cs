using AoC.Games.Games.Deflectors;
using AoC.Games.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace AoC.Games.Games.Mazes;

[UsedImplicitly]
public class Game : Microsoft.Xna.Framework.Game
{
    private const int Width = 51;

    private const int Height = 51;

    private const int TileSize = 10;

    private readonly bool[,] _maze = new bool[Width, Height];
    
    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;

    private readonly Stack<(int X, int Y)> _stack = [];

    private readonly HashSet<(int X, int Y)> _visited = [];
    
    private (int X, int Y) _position;

    private (int Dx, int Dy) _direction;
    
    private int _move;

    private readonly Random _rng = new();
    
    public Game()
    {
        var scaleFactor = AppSettings.Instance.ScaleFactor;

        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = (int) (Constants.BufferWidth * scaleFactor),
            PreferredBackBufferHeight = (int) (Constants.BufferHeight * scaleFactor)
        };

        Content.RootDirectory = "./Deflectors";
    }

    protected override void Initialize()
    {
        _position = (1, 0);

        _direction = (0, 1);
        
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        _stack.Push(_position);

        _visited.Add(_position);

        _maze[_position.X, _position.Y] = true;

        if (_move > 0 && _move % 2 == 0)
        {
            var directions = GetDirections();

            _direction = directions[_rng.Next(directions.Count)];
        }
        else
        {
            _position.X += _direction.Dx;
            _position.Y += _direction.Dy;
        }

        _move++;

        base.Update(gameTime);
    }

    private List<(int Dx, int Dy)> GetDirections()
    {
        var directions = new List<(int Dx, int Dy)>();
        
        if (_position.X > 2 && _visited.Contains((_position.X - 1, _position.Y)))
        {
            directions.Add((-1, 0));
        }

        if (_position.X < Width - 3 && _visited.Contains((_position.X + 1, _position.Y)))
        {
            directions.Add((1, 0));
        }

        if (_position.Y > 2 && _visited.Contains((_position.X, _position.Y - 1)))
        {
            directions.Add((0, -1));
        }

        if (_position.Y < Height - 3 && _visited.Contains((_position.X, _position.Y + 1)))
        {
            directions.Add((0, 1));
        }

        return directions;
    }
}