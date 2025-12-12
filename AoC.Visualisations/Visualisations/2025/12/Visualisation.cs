using AoC.Solutions.Infrastructure;
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

    private int[][] _grid;

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

    private int _presentIndex;

    private Coordinate _lastPlacement;

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

            _presentIndex = 0;

            _lastPlacement = new Coordinate(-1, -1);

            _grid = new int[_area.Height][];

            for (var y = 0; y < _area.Height; y++)
            {
                _grid[y] = new int[_area.Width];
            }

            _needArea = false;
        }

        PlaceNextTile();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        Array.Fill(_data, Color.Black);

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

        for (var y = 0; y < _area.Height; y++)
        {
            for (var x = 0; x < _area.Width; x++)
            {
                if (_grid[y][x] > 0)
                {
                    var tileId = _grid[y][x];

                    var color = GetTileColor(tileId);

                    for (var py = 1; py < TileHeight; py++)
                    {
                        for (var px = 1; px < TileWidth; px++)
                        {
                            this[x * TileWidth + px, y * TileHeight + py] = color;
                        }
                    }
                }
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

    private static Color GetTileColor(int tileId)
    {
        var hue = tileId * 137.508f % 360;

        return ColorFromHsv(hue, 0.7f, 0.9f);
    }

    private static Color ColorFromHsv(float hue, float saturation, float value)
    {
        var hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;

        var f = hue / 60 - Math.Floor(hue / 60);

        value *= 255;

        var v = Convert.ToInt32(value);

        var p = Convert.ToInt32(value * (1 - saturation));

        var q = Convert.ToInt32(value * (1 - f * saturation));

        var t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => new Color(v, t, p),
            1 => new Color(q, v, p),
            2 => new Color(p, v, t),
            3 => new Color(p, q, v),
            4 => new Color(t, p, v),
            _ => new Color(v, p, q)
        };
    }

    private void PlaceNextTile()
    {
        var tile = GetNextTile();

        if (tile == null)
        {
            _needArea = true;

            return;
        }

        Coordinate position;

        if (_lastPlacement.X == -1)
        {
            position = new Coordinate(0, 0);
        }
        else
        {
            position = _lastPlacement with { X = _lastPlacement.X + 2 };
            
            if (! CanPlace(position, tile))
            {
                position = _lastPlacement with { X = _lastPlacement.X + 3 };

                if (! CanPlace(position, tile))
                {
                    position = new Coordinate(0, _lastPlacement.Y + 3);

                    if (! CanPlace(position, tile))
                    {
                        return;
                    }
                }
            }
        }

        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                if (tile[y][x])
                {
                    _grid[position.Y + y][position.X + x] = _presentIndex;
                }
            }
        }

        _lastPlacement = position;
    }

    private bool CanPlace(Coordinate position, bool[][] tile)
    {
        if (position.X < 0 || position.X > _area.Width - 3 || position.Y < 0 || position.Y > _area.Height - 3)
        {
            return false;
        }

        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                if (tile[y][x] && _grid[position.Y + y][position.X + x] != 0)
                {
                    return false;
                }
            }
        }

        return true;
    }


    private bool[][] GetNextTile()
    {
        var startIndex = _presentIndex;

        do
        {
            if (_area.PresentCounts[_presentIndex] > 0)
            {
                var tile = _puzzleState.Tiles[_presentIndex];

                _area.PresentCounts[_presentIndex]--;

                _presentIndex = (_presentIndex + 1) % _area.PresentCounts.Length;

                return tile;
            }

            _presentIndex = (_presentIndex + 1) % _area.PresentCounts.Length;
        } while (_presentIndex != startIndex);

        return null;
    }

    private Color this[int x, int y]
    {
        set => _data[(_top + y) * CanvasWidth + _left + x] = value;
    }
}