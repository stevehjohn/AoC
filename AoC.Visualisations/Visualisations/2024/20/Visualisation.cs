using AoC.Solutions.Common;
using AoC.Solutions.Solutions._2024._20;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2024._20;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int TileWidth = 8;

    private const int TileHeight = 6;

    private const int FrameIncrement = 1000;

    private readonly Color[] _data = new Color[PuzzleState.Size * TileWidth * PuzzleState.Size * TileHeight];

    private readonly Queue<PuzzleState> _stateQueue = [];

    private PuzzleState _state;

    private Texture2D _texture;

    private SpriteBatch _spriteBatch;

    private int _position;

    private int _mode;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = PuzzleState.Size * TileWidth,
            PreferredBackBufferHeight = PuzzleState.Size * TileHeight
        };

        Content.RootDirectory = "./20";
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

    protected override void Initialize()
    {
        IsMouseVisible = true;

        _texture = new Texture2D(GraphicsDeviceManager.GraphicsDevice, PuzzleState.Size * TileWidth, PuzzleState.Size * TileHeight);

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
            if (_mode == 0 || _mode == 3)
            {
                _state = _stateQueue.Dequeue();

                if (_mode == 0)
                {
                    _mode = 1;
                }
            }

            if (_mode == 2)
            {
                _mode = 3;
            }
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

        for (var i = 0; i < _position; i++)
        {
            var cell = PuzzleState.Track[i];

            DrawTile(cell.X, cell.Y, 1, Color.FromNonPremultiplied(0, 192, 0, 255));
        }

        if (_position < PuzzleState.Track.Count)
        {
            _position += FrameIncrement;

            if (_position > PuzzleState.Track.Count)
            {
                _position = PuzzleState.Track.Count;

                _mode = 2;
            }
        }

        if (_state.ShortcutStart != Point2D.Null)
        {
            var render = false;
            
            for (var i = 0; i < PuzzleState.Track.Count; i++)
            {
                if (PuzzleState.Track[i] == _state.ShortcutEnd)
                {
                    render = true;
                }

                if (PuzzleState.Track[i] == _state.ShortcutStart)
                {
                    break;
                }

                if (! render)
                {
                    continue;
                }

                var cell = PuzzleState.Track[i]; 
                
                DrawTile(cell.X, cell.Y, 1, Color.FromNonPremultiplied(130, 90, 30, 255));
            }
        }

        _texture.SetData(_data);
        
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        _spriteBatch.Draw(_texture, 
            new Rectangle(0, 0, PuzzleState.Size * TileWidth, PuzzleState.Size * TileHeight), 
            new Rectangle(0, 0, PuzzleState.Size * TileWidth, PuzzleState.Size * TileHeight), Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawTile(int x, int y, int border, Color color)
    {
        for (var tX = border; tX < TileWidth - border; tX++)
        {
            for (var tY = border; tY < TileHeight - border; tY++)
            {
                _data[x * TileWidth + tX + (y * TileHeight + tY) * PuzzleState.Size * TileWidth] = color;
            }
        }
    }
}