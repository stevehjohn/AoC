using AoC.Solutions.Solutions._2025._12;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2025._12;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int Width = 50;

    private const int Height = 50;

    private const int TileWidth = 18;

    private const int TileHeight = 18;

    private const int CanvasWidth = Width * TileWidth + 1;

    private const int CanvasHeight = Height * TileHeight + 1;

    private int[,] _grid = new int[50, 50];

    private SpriteBatch _spriteBatch;

    private Texture2D _texture;

    private readonly Color[] _data = new Color[CanvasWidth * CanvasHeight];

    private Area _area;

    private int _areaIndex;

    private bool _needArea = true;

    private PuzzleState _puzzleState;

    private int _width;

    private int _height;

    private int _left;

    private int _top;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = CanvasWidth,
            PreferredBackBufferHeight = CanvasHeight
        };

        Content.RootDirectory = "./12";
    }

    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            1 => new Part1(this),
            _ => throw new VisualisationParameterException()
        };
    }

    protected override void Initialize()
    {
        IsMouseVisible = true;

        _texture = new Texture2D(GraphicsDeviceManager.GraphicsDevice, CanvasWidth, CanvasHeight);

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            _puzzleState = GetNextState();
        }

        if (_needArea)
        {
            _area = _puzzleState.Areas[_areaIndex++];

            _width = _area.Width * TileWidth;

            _height = _area.Height * TileHeight;

            _left = CanvasWidth / 2 - _width / 2;

            _top = CanvasHeight / 2 - _height / 2;

            _needArea = false;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        for (var y = 0; y <= _area.Height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                this[x, y * TileHeight] = Color.Gray;
            }
        }

        for (var x = 0; x <= _area.Width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                this[x * TileWidth, y] = Color.Gray;
            }
        }

        _texture.SetData(_data);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        _spriteBatch.Draw(_texture,
            new Rectangle(0, 0, CanvasWidth, CanvasHeight),
            new Rectangle(0, 0, CanvasWidth, CanvasHeight),
            Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private Color this[int x, int y]
    {
        set => _data[(_top + y) * CanvasWidth + _left + x] = value;
    }
}