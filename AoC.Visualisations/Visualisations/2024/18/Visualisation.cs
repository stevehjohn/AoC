using AoC.Solutions.Common;
using AoC.Solutions.Solutions._2024._18;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2024._18;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int TileSize = 12;

    private readonly Color[] _data = new Color[PuzzleState.Size * TileSize * PuzzleState.Size * TileSize];

    private readonly Queue<PuzzleState> _stateQueue = [];

    private readonly Dictionary<Point2D, int> _newPoints = [];

    private PuzzleState _state;

    private Texture2D _texture;

    private SpriteBatch _spriteBatch;

    private char[,] _map;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = PuzzleState.Size * TileSize,
            PreferredBackBufferHeight = PuzzleState.Size * TileSize
        };

        Content.RootDirectory = "./15";
    }
    
    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            2 => new Part2(this),
            _ => throw new VisualisationParameterException()
        };
    }

    protected override void Initialize()
    {
        IsMouseVisible = true;

        _texture = new Texture2D(GraphicsDeviceManager.GraphicsDevice, PuzzleState.Size * TileSize, PuzzleState.Size * TileSize);

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            _stateQueue.Enqueue(GetNextState());
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Array.Fill(_data, Color.Black);

        if (_stateQueue.Count > 0)
        {
             _state = _stateQueue.Dequeue();

            _map ??= PuzzleState.Map;
        }

        if (_state == null)
        {
            return;
        }

        for (var y = 0; y < PuzzleState.Size; y++)
        {
            for (var x = 0; x < PuzzleState.Size; x++)
            {
                if (PuzzleState.Map[x, y] == '#')
                {
                    DrawTile(x, y, 0, Color.FromNonPremultiplied(64, 64, 64, 255));
                }
            }
        }

        if (_state.NewPoint != default)
        {
            _newPoints.TryAdd(_state.NewPoint, 0);
        }

        Point2D remove = default;
        
        foreach (var point in _newPoints)
        {
            var color = Color.FromNonPremultiplied(255 - point.Value * 3, 128 - point.Value, 128 - point.Value, 255);
            
            DrawTile(point.Key.X, point.Key.Y, 0, color);

            _newPoints[point.Key]++;

            if (_newPoints[point.Key] > 63)
            {
                remove = point.Key;
            }
        }

        if (remove != default)
        {
            _newPoints.Remove(remove);

            _map[remove.X, remove.Y] = '#';
        }

        foreach (var point in _state.Visited)
        {
            DrawTile(point.X, point.Y, 1, Color.FromNonPremultiplied(161, 110, 0, 255));
        }
        
        foreach (var point in _state.Path)
        {
            DrawTile(point.X, point.Y, 1, Color.FromNonPremultiplied(0, 192, 0, 255));
        }

        _texture.SetData(_data);
        
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        _spriteBatch.Draw(_texture, 
            new Rectangle(0, 0, PuzzleState.Size * TileSize, PuzzleState.Size * TileSize), 
            new Rectangle(0, 0, PuzzleState.Size * TileSize, PuzzleState.Size * TileSize), Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawTile(int x, int y, int border, Color color)
    {
        for (var tX = border; tX < TileSize - border; tX++)
        {
            for (var tY = border; tY < TileSize - border; tY++)
            {
                _data[x * TileSize + tX + (y * TileSize + tY) * PuzzleState.Size * TileSize] = color;
            }
        }
    }
}