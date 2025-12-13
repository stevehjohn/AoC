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
    private const int Pause = 750;
    
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

    private Color _backgroundColour = Color.Black;

    private double _lastCompletion;

    private readonly Random _random = new Random();

    private List<Coordinate> _availablePositions;

    private HashSet<(long x, long y)> _filledCells;

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

        switch (_needArea)
        {
            case true when _areaIndex > 0 && gameTime.TotalGameTime.TotalMilliseconds - _lastCompletion < Pause:
                base.Update(gameTime);
            
                return;
            
            case true when _areaIndex >= _puzzleState.Areas.Count:
                return;
            
            case true:
            {
                _area = _puzzleState.Areas[_areaIndex++];

                _width = _area.Width * TileWidth;

                _height = _area.Height * TileHeight;

                _left = CanvasWidth / 2 - _width / 2;

                _top = CanvasHeight / 2 - _height / 2;

                _presentIndex = 0;

                _grid = new int[_area.Height][];

                for (var y = 0; y < _area.Height; y++)
                {
                    _grid[y] = new int[_area.Width];
                }

                _filledCells = new HashSet<(long x, long y)>();
            
                var centerX = _area.Width / 2;

                var centerY = _area.Height / 2;
            
                var startX = centerX + _random.Next(-2, 3);
            
                var startY = centerY + _random.Next(-2, 3);
            
                _availablePositions =
                [
                    new Coordinate(Math.Clamp(startX, 0, _area.Width - 3), Math.Clamp(startY, 0, _area.Height - 3))
                ];

                _backgroundColour = Color.Black;
            
                _needArea = false;
                
                break;
            }
        }

        PlaceNextTile(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Array.Fill(_data, _backgroundColour);

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

    private void PlaceNextTile(GameTime gameTime)
    {
        var tile = GetNextTile();

        if (tile == null)
        {
            if (_area.IsValid)
            {
                _backgroundColour = Color.FromNonPremultiplied(0, 64, 0, 255);
            }

            _needArea = true;

            _lastCompletion = gameTime.TotalGameTime.TotalMilliseconds;
            
            return;
        }

        var positionsToTry = new List<Coordinate>(_availablePositions);
        
        for (var i = positionsToTry.Count - 1; i > 0; i--)
        {
            var j = _random.Next(i + 1);
            
            (positionsToTry[i], positionsToTry[j]) = (positionsToTry[j], positionsToTry[i]);
        }

        foreach (var position in positionsToTry)
        {
            foreach (var orientation in GetAllOrientations(tile))
            {
                if (CanPlace(position, orientation))
                {
                    PlaceTile(position, orientation);
                    
                    UpdateAvailablePositions(position);
                    
                    return;
                }
            }
        }

        var allPositions = new List<Coordinate>();
        
        for (var y = 0; y < _area.Height - 2; y++)
        {
            for (var x = 0; x < _area.Width - 2; x++)
            {
                allPositions.Add(new Coordinate(x, y));
            }
        }

        for (var i = allPositions.Count - 1; i > 0; i--)
        {
            var j = _random.Next(i + 1);
            
            (allPositions[i], allPositions[j]) = (allPositions[j], allPositions[i]);
        }

        foreach (var position in allPositions)
        {
            foreach (var orientation in GetAllOrientations(tile))
            {
                if (CanPlace(position, orientation))
                {
                    PlaceTile(position, orientation);
                    
                    UpdateAvailablePositions(position);
                    
                    return;
                }
            }
        }

        if (! _area.IsValid)
        {
            _backgroundColour = Color.FromNonPremultiplied(64, 0, 0, 255);
        }

        _needArea = true;

        _lastCompletion = gameTime.TotalGameTime.TotalMilliseconds;
    }

    private void PlaceTile(Coordinate position, bool[][] tile)
    {
        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                if (tile[y][x])
                {
                    _grid[position.Y + y][position.X + x] = _presentIndex;
                    _filledCells.Add((position.X + x, position.Y + y));
                }
            }
        }
    }

    private void UpdateAvailablePositions(Coordinate placed)
    {
        _availablePositions.Remove(placed);

        var offsets = new[] 
        { 
            (-3, 0), (3, 0), (0, -3), (0, 3),
            (-2, 0), (2, 0), (0, -2), (0, 2),
            (-1, 0), (1, 0), (0, -1), (0, 1),
            (-2, -2), (-2, 2), (2, -2), (2, 2),
            (-1, -1), (-1, 1), (1, -1), (1, 1)
        };

        foreach (var (dx, dy) in offsets)
        {
            var newPos = new Coordinate(placed.X + dx, placed.Y + dy);
            
            if (newPos.X >= 0 && newPos.X <= _area.Width - 3 && newPos.Y >= 0 && newPos.Y <= _area.Height - 3 && !_availablePositions.Contains(newPos))
            {
                var hasAdjacentFilled = false;
                
                for (var y = 0; y < 3 && !hasAdjacentFilled; y++)
                {
                    for (var x = 0; x < 3 && !hasAdjacentFilled; x++)
                    {
                        if (_filledCells.Contains((newPos.X + x, newPos.Y + y)))
                        {
                            hasAdjacentFilled = true;
                        }
                    }
                }
                
                if (hasAdjacentFilled)
                {
                    _availablePositions.Add(newPos);
                }
            }
        }
    }

    private IEnumerable<bool[][]> GetAllOrientations(bool[][] tile)
    {
        var current = tile;
        
        var startRotation = _random.Next(4);
    
        for (var i = 0; i < startRotation; i++)
        {
            current = Rotate90(current);
        }
    
        for (var i = 0; i < 4; i++)
        {
            yield return current;
            
            current = Rotate90(current);
        }
    
        current = FlipHorizontal(tile);
        
        for (var i = 0; i < 4; i++)
        {
            yield return current;
            
            current = Rotate90(current);
        }
    }

    private static bool[][] Rotate90(bool[][] tile)
    {
        var rotated = new bool[3][];

        for (var i = 0; i < 3; i++)
        {
            rotated[i] = new bool[3];
        }

        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                rotated[x][2 - y] = tile[y][x];
            }
        }

        return rotated;
    }

    private static bool[][] FlipHorizontal(bool[][] tile)
    {
        var flipped = new bool[3][];

        for (var i = 0; i < 3; i++)
        {
            flipped[i] = new bool[3];
        }

        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                flipped[y][2 - x] = tile[y][x];
            }
        }

        return flipped;
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

    private Color this[int x, int y]
    {
        set => _data[(_top + y) * CanvasWidth + _left + x] = value;
    }
}