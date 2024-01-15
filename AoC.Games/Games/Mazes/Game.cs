using AoC.Games.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Mazes;

[UsedImplicitly]
public class Game : Microsoft.Xna.Framework.Game
{
    private readonly bool[,] _maze = new bool[Constants.Width, Constants.Height];

    private readonly bool[,] _mazeSolution = new bool[Constants.Width, Constants.Height];
    
    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;
    
    private State _state;

    private Texture2D _texture;
    
    private readonly Color[] _data = new Color[Constants.Width * Constants.TileSize * Constants.Height * Constants.TileSize];

    private SpriteBatch _spriteBatch;

    private readonly Input _input = new();

    private MazeCreator _mazeCreator;

    private MazeSolver _mazeSolver;

    private List<(int X, int Y)> _solution;

    private int _step;
    
    public Game()
    {
        var scaleFactor = AppSettings.Instance.ScaleFactor;

        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = (int) (Constants.Width * Constants.TileSize * scaleFactor),
            PreferredBackBufferHeight = (int) (Constants.Height * Constants.TileSize * scaleFactor)
        };

        Content.RootDirectory = "./Deflectors";
    }

    protected override void Initialize()
    {
        _mazeCreator = new MazeCreator(_maze);

        _mazeSolver = new MazeSolver(_maze);
        
        _mazeCreator.Reset();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _texture = new Texture2D(GraphicsDevice, Constants.Width * Constants.TileSize, Constants.Height * Constants.TileSize);
        
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (_input.LeftButtonClicked())
        {
            _state = State.Creating;
            
            _mazeCreator.Reset();
        }
        
        switch (_state)
        {
            case State.Creating:
                if (_mazeCreator.CreateMaze())
                {
                    _state = State.Created;
                }

                break;
            
            case State.Created:
                _state = State.Solving;
                
                break;
            
            case State.Solving:
                _solution = _mazeSolver.SolveMaze();

                _state = State.Solved;
                
                break;
            
            case State.Solved:
                var step = _solution[_step];

                _mazeSolution[step.X, step.Y] = true;

                if (_step < _solution.Count - 1)
                {
                    _step++;
                }
                else
                {
                    _state = State.Finished;
                }

                break;
        }
        
        _input.UpdateState();
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        DrawIntoData();
        
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack);

        _texture.SetData(_data);
        
        _spriteBatch.Draw(_texture, new Vector2(0, 0), new Rectangle(0, 0, Constants.Width * Constants.TileSize, Constants.Height * Constants.TileSize), Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawIntoData()
    {
        for (var x = 0; x < Constants.Width * Constants.TileSize; x++)
        {
            for (var y = 0; y < Constants.Height * Constants.TileSize; y++)
            {
                if (_mazeSolution[x / Constants.TileSize, y / Constants.TileSize])
                {
                    _data[x + y * Constants.Width * Constants.TileSize] = Color.FromNonPremultiplied(0, 128, 0, 255);
                    
                    continue;
                }

                if (! _maze[x / Constants.TileSize, y / Constants.TileSize])
                {
                    _data[x + y * Constants.Width * Constants.TileSize] = Color.FromNonPremultiplied(64, 64, 64, 255);
                }
                else
                {
                    _data[x + y *Constants.Width * Constants.TileSize] = Color.Black;
                }
            }
        }
    }
}