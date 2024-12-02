using AoC.Solutions.Solutions._2023._14;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace AoC.Visualisations.Visualisations._2023._14;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private SpriteBatch _spriteBatch;
    
    private Rock[,] _map;
    
    private PuzzleState _state;

    private Texture2D _sprites;

    private int _cycle;

    private int _width;

    private int _height;

    private int _totalCycles;

    private bool _solved;
    
    private readonly Dictionary<int, int> _hashes = new();
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 700,
                                     PreferredBackBufferHeight = 700
                                 };

        Content.RootDirectory = "./14";

        IgnoreQueueLimit = true;
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

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sprites = Content.Load<Texture2D>("sprites");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            var state = GetNextState();

            _width = state.Map.GetLength(0);

            _height = state.Map.GetLength(1);
            
            _map = new Rock[_width, _height];

            var rng = new Random();

            var palette = PaletteGenerator.GetPalette(26,
            [
                new Color(46, 27, 134),
                    new Color(119, 35, 172),
                    new Color(176, 83, 203),
                    new Color(255, 168, 76),
                    new Color(254, 211, 56),
                    new Color(254, 253, 0)
            ]);

            var i = 0;
            
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    if (state.Map[x, y] != '.')
                    {
                        if (state.Map[x, y] == 'O')
                        {
                            i++;
                        }

                        _map[x, y] = new Rock
                        {
                            Round = state.Map[x, y] == 'O',
                            Color = i == 150 ? Color.Red : palette[rng.Next(26)]
                        };
                    }
                }
            }

            _state = state;
        }
        else
        {
            if (_totalCycles == 1_000_000_000)
            {
                Console.WriteLine($"\nAnswer: {GetLoad()}\n\n");

                _totalCycles++;

                _cycle = 4;
                
                base.Update(gameTime);
                
                return;
            }

            switch (_cycle)
            {
                case 0:
                    if (! UpdateMap(0, -1))
                    {
                        _cycle = 1;
                    }
                    break;

                case 1:
                    if (! UpdateMap(-1, 0))
                    {
                        _cycle = 2;
                    }
                    break;

                case 2:
                    if (! UpdateMap(0, 1))
                    {
                        _cycle = 3;
                    }
                    break;

                case 3:
                    if (! UpdateMap(1, 0))
                    {
                        _cycle = 0;

                        _totalCycles++;

                        var hash = GetHash();

                        Console.Write($"Cycle: {_totalCycles,13:N0}    Load: {GetLoad(),7:N0}    Hash: {hash:X8}\n");

                        if (! _hashes.TryAdd(hash, _totalCycles) && ! _solved)
                        {
                            _solved = true;
                            
                            var seenCycle = _hashes[hash];
                            
                            Console.WriteLine("\nDéjà vu.");

                            Console.WriteLine($"Hash seen before on cycle {seenCycle:N0}");

                            var cycleLength = _totalCycles - seenCycle;
                            
                            Console.WriteLine($"Cycle length is: {cycleLength}");

                            var remainingCycles = (1_000_000_000 - _totalCycles) % cycleLength;

                            var skip = 1_000_000_000 - _totalCycles - remainingCycles;
                            
                            Console.WriteLine($"We can now skip {skip:N0} cycles.\n");

                            _totalCycles += skip;
                        }
                    }
                    
                    break;
            }
        }

        base.Update(gameTime);
    }

    private int GetHash()
    {
        var hash = 0;
        
        for (var y = 0; y < _height; y++)
        {
            var hashString = new char[_width];
            
            for (var x = 0; x < _width; x++)
            {
                var item = _map[x, y];

                hashString[x] = item == null
                    ? '.'
                    : item.Round
                        ? 'O'
                        : '#';
            }

            hash = HashCode.Combine(hash, new string(hashString).GetHashCode());
        }

        return hash;
    }

    private bool UpdateMap(int dX, int dY)
    {
        var moved = false;

        var sY = dY <= 0 ? 0 : _height - 1;
        
        var eY = dY <= 0 ? _height : -1;

        var stepY = sY < eY ? 1 : -1;

        var sX = dX <= 0 ? 0 : _width - 1;
        
        var eX = dX <= 0 ? _width : -1;

        var stepX = sX < eX ? 1 : -1;
        
        for (var x = sX; x != eX; x += stepX)
        {
            for (var y = sY; y != eY; y += stepY)
            {
                var rock = _map[x, y];

                if (rock is not { Round: true })
                {
                    continue;
                }

                var nX = x + dX;

                var nY = y + dY;

                if (nX < 0 || nX >= _width)
                {
                    continue;
                }

                if (nY < 0 || nY >= _height)
                {
                    continue;
                }

                if (_map[nX, nY] == null)
                {
                    _map[nX, nY] = rock;

                    _map[x, y] = null;

                    moved = true;
                }
            }
        }

        return moved;
    }

    protected override void Draw(GameTime gameTime)
    {
        if (_state != null)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            DrawState();
            
            _spriteBatch.End();
        }
        else
        {
            GraphicsDevice.Clear(Color.Black);
        }

        base.Draw(gameTime);
    }

    private void DrawState()
    {
        for (var y = 0; y < _width; y++)
        {
            for (var x = 0; x < _height; x++)
            {
                if (_map[x, y] == null)
                {
                    continue;
                }

                var rock = _map[x, y];

                var sprite = rock.Round ? new Rectangle(0, 0, 7, 7) : new Rectangle(7, 0, 7, 7);

                var color = rock.Round ? _map[x, y].Color : Color.FromNonPremultiplied(80, 80, 80, 255);
                
                _spriteBatch.Draw(_sprites, new Vector2(x * 7, y * 7), sprite, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
            }
        }
    }
    
    private int GetLoad()
    {
        var load = 0;
        
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (_map[x, y] != null && _map[x, y].Round)
                {
                    load += _height - y;
                }
            }
        }

        return load;
    }
}