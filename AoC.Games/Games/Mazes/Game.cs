using AoC.Games.Infrastructure;
using AoC.Solutions.Extensions;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Mazes;

[UsedImplicitly]
public class Game : Microsoft.Xna.Framework.Game
{
    private const int StepsPerFrame = 3;
    
    private readonly bool[,] _maze = new bool[Constants.Width, Constants.Height];

    private readonly bool[,] _mazeSolution = new bool[Constants.Width, Constants.Height];

    private readonly bool[,] _mazeVisited = new bool[Constants.Width, Constants.Height];

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

    private List<(int X, int Y)> _visited;

    private int _step;

    public Game()
    {
        var scaleFactor = AppSettings.Instance.ScaleFactor;

        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = (int) (Constants.Width * Constants.TileSize * scaleFactor),
            PreferredBackBufferHeight = (int) (Constants.Height * Constants.TileSize * scaleFactor)
        };
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
        if (IsActive && _input.LeftButtonClicked() && _input.MouseY > 0 && _input.MouseX > 0 && _input.MouseX < GraphicsDevice.Viewport.Width && _input.MouseY < GraphicsDevice.Viewport.Height)
        {
            Reset();
        }

        (int X, int Y) step;
        
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (_state)
        {
            case State.Creating:
                for (var i = 0; i < StepsPerFrame; i++)
                {
                    if (_mazeCreator.CreateMaze())
                    {
                        _state = State.Created;
                        
                        break;
                    }
                }

                break;

            case State.Created:
                _state = State.Solving;

                break;

            case State.Solving:
                (_solution, _visited) = _mazeSolver.SolveMaze();

                _state = State.Visiting;

                break;
            
            case State.Visiting:
                for (var i = 0; i < StepsPerFrame; i++)
                {
                    step = _visited[_step];

                    _mazeVisited[step.X, step.Y] = true;

                    if (_step < _visited.Count - 1)
                    {
                        _step++;
                    }
                    else
                    {
                        _step = 0;

                        _state = State.Solved;
                    }
                }

                break;

            case State.Solved:
                for (var i = 0; i < StepsPerFrame; i++)
                {
                    step = _solution[_step];

                    _mazeSolution[step.X, step.Y] = true;

                    if (_step < _solution.Count - 1)
                    {
                        _step++;
                    }
                    else
                    {
                        _state = State.Finished;
                    }
                }

                break;
        }

        _input.UpdateState();

        base.Update(gameTime);
    }

    private void Reset()
    {
        _state = State.Creating;

        _solution = null;

        _visited = null;
        
        _maze.ForAll((x, y, _) =>
        {
            _mazeSolution[x, y] = false;
            _mazeVisited[x, y] = false;
        });

        _step = 0;

        _mazeCreator.Reset();
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
                    if (x % Constants.TileSize > 2 && x % Constants.TileSize < Constants.TileSize - 3
                        && y % Constants.TileSize > 2 && y % Constants.TileSize < Constants.TileSize - 3)
                    {
                        _data[x + y * Constants.Width * Constants.TileSize] = Color.FromNonPremultiplied(0, 192, 0, 255);

                        continue;
                    }
                    
                    if (x % Constants.TileSize > 1 && x % Constants.TileSize < Constants.TileSize - 2 
                        && y % Constants.TileSize > 1 && y % Constants.TileSize < Constants.TileSize - 2)
                    {
                        _data[x + y * Constants.Width * Constants.TileSize] = Color.FromNonPremultiplied(0, 96, 0, 255);

                        continue;
                    }
                }

                if (_mazeVisited[x / Constants.TileSize, y / Constants.TileSize])
                {
                    if (x % Constants.TileSize > 3 && x % Constants.TileSize < Constants.TileSize - 4 
                        && y % Constants.TileSize > 3 && y % Constants.TileSize < Constants.TileSize - 4)
                    {
                        _data[x + y * Constants.Width * Constants.TileSize] = Color.FromNonPremultiplied(191, 127, 0, 255);

                        continue;
                    }
                    
                    if (x % Constants.TileSize > 2 && x % Constants.TileSize < Constants.TileSize - 3 
                        && y % Constants.TileSize > 2 && y % Constants.TileSize < Constants.TileSize - 3)
                    {
                        _data[x + y * Constants.Width * Constants.TileSize] = Color.FromNonPremultiplied(127, 63, 0, 255);

                        continue;
                    }
                }

                if (! _maze[x / Constants.TileSize, y / Constants.TileSize])
                {
                    _data[x + y * Constants.Width * Constants.TileSize] = Color.FromNonPremultiplied(64, 64, 64, 255);
                }
                else
                {
                    _data[x + y * Constants.Width * Constants.TileSize] = Color.Black;
                }
            }
        }
    }
}