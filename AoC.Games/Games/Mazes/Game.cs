using AoC.Games.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
    
    private readonly Random _rng = new();

    private bool _complete;

    private Texture2D _texture;
    
    private readonly Color[] _data = new Color[Width * TileSize * Height * TileSize];

    private SpriteBatch _spriteBatch;

    public Game()
    {
        var scaleFactor = AppSettings.Instance.ScaleFactor;

        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = (int) (Width * TileSize * scaleFactor),
            PreferredBackBufferHeight = (int) (Height * TileSize * scaleFactor)
        };

        Content.RootDirectory = "./Deflectors";
    }

    protected override void Initialize()
    {
        _position = (1, 0);

        _direction = (0, 1);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _texture = new Texture2D(GraphicsDevice, Width * TileSize, Height * TileSize);
        
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (_complete)
        {
            return;
        }
        
        _stack.Push(_position);

        _visited.Add(_position);

        _maze[_position.X, _position.Y] = true;

        var directions = GetDirections();

        while (directions.Count == 0)
        {
            if (! _stack.TryPop(out var position))
            {
                _complete = true;
            }
            else
            {
                _position = position;
            }

            directions = GetDirections();
        }
        
        _direction = directions[_rng.Next(directions.Count)];

        _position.X += _direction.Dx;
        _position.Y += _direction.Dy;

        _maze[_position.X, _position.Y] = true;

        _position.X += _direction.Dx;
        _position.Y += _direction.Dy;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        DrawIntoData();
        
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack);

        _texture.SetData(_data);
        
        _spriteBatch.Draw(_texture, new Vector2(0, 0), new Rectangle(0, 0, Width * TileSize, Height * TileSize), Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawIntoData()
    {
        for (var x = 0; x < Width * TileSize; x++)
        {
            for (var y = 0; y < Height * TileSize; y++)
            {
                if (! _maze[x / TileSize, y / TileSize])
                {
                    _data[x + y * Width * TileSize] = Color.DarkMagenta;
                }
                else
                {
                    _data[x + y * Width * TileSize] = Color.Black;
                }
            }
        }
    }

    private List<(int Dx, int Dy)> GetDirections()
    {
        var directions = new List<(int Dx, int Dy)>();
        
        if (_position.X > 3 && ! _visited.Contains((_position.X - 1, _position.Y)))
        {
            directions.Add((-1, 0));
        }

        if (_position.X < Width - 4 && ! _visited.Contains((_position.X + 1, _position.Y)))
        {
            directions.Add((1, 0));
        }

        if (_position.Y > 3 && ! _visited.Contains((_position.X, _position.Y - 1)))
        {
            directions.Add((0, -1));
        }

        if (_position.Y < Height - 4 && ! _visited.Contains((_position.X, _position.Y + 1)))
        {
            directions.Add((0, 1));
        }

        return directions;
    }
}