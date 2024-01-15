using AoC.Games.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Mazes;

[UsedImplicitly]
public class Game : Microsoft.Xna.Framework.Game
{
    private const int Width = 201;

    private const int Height = 141;

    private const int TileSize = 5;

    private bool[,] _maze = new bool[Width, Height];
    
    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;

    private readonly Stack<(int X, int Y)> _stack = [];

    private readonly HashSet<(int X, int Y)> _visited = [];
    
    private (int X, int Y) _position;

    private (int Dx, int Dy) _direction;
    
    private readonly Random _rng = new();

    private State _state;

    private Texture2D _texture;
    
    private readonly Color[] _data = new Color[Width * TileSize * Height * TileSize];

    private SpriteBatch _spriteBatch;

    private int _move;

    private readonly Input _input = new();
    
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
        Reset();
        
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
        if (_input.LeftButtonClicked())
        {
            Reset();
        }
        
        switch (_state)
        {
            case State.Creating:
                CreateMaze();

                break;
            
            case State.Created:
                _state = State.Solving;
                
                break;
            
            case State.Solving:
                SolveMaze();
                
                break;
            
            case State.Solved:
                break;
        }
     
        _move++;
        
        _input.UpdateState();
        
        base.Update(gameTime);
    }

    private void Reset()
    {
        _position = (1, -1);

        _direction = (0, 1);

        _move = 0;

        _maze = new bool[Width, Height];
        
        _visited.Clear();
        
        _stack.Clear();

        _state = State.Creating;
    }

    private void SolveMaze()
    {

        _state = State.Solved;
    }

    private void CreateMaze()
    {
        if (_position.Y >= 0)
        {
            _stack.Push(_position);

            _visited.Add(_position);

            _maze[_position.X, _position.Y] = true;
        }

        if (_move > 0)
        {
            var directions = GetDirections();

            while (directions.Count == 0)
            {
                if (! _stack.TryPop(out var position))
                {
                    _maze[Width - 2, Height - 1] = true;
                    
                    _state = State.Created;
                    
                    return;
                }
                
                _position = position;

                directions = GetDirections();
            }

            _direction = directions[_rng.Next(directions.Count)];
        }

        _position.X += _direction.Dx;
        _position.Y += _direction.Dy;

        _maze[_position.X, _position.Y] = true;

        _position.X += _direction.Dx;
        _position.Y += _direction.Dy;
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
                    _data[x + y * Width * TileSize] = Color.FromNonPremultiplied(64, 64, 64, 255);
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
        
        if (_position.X > 2 && ! _visited.Contains((_position.X - 2, _position.Y)))
        {
            directions.Add((-1, 0));
        }

        if (_position.X < Width - 3 && ! _visited.Contains((_position.X + 2, _position.Y)))
        {
            directions.Add((1, 0));
        }

        if (_position.Y > 2 && ! _visited.Contains((_position.X, _position.Y - 2)))
        {
            directions.Add((0, -1));
        }

        if (_position.Y < Height - 3 && ! _visited.Contains((_position.X, _position.Y + 2)))
        {
            directions.Add((0, 1));
        }

        return directions;
    }
}