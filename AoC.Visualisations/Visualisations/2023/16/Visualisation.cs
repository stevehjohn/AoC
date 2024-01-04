using AoC.Solutions.Solutions._2023._16;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace AoC.Visualisations.Visualisations._2023._16;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int FastFrameStart = 4_000;
    
    private SpriteBatch _spriteBatch;
    
    private Texture2D _tiles;

    private PuzzleState _state;

    private readonly Dictionary<int, List<(int X, int Y, char Direction, char Tile, int SourceId)>> _allBeams = new();

    private List<Segment> _segments = [];

    private readonly List<Segment> _bestSegments = [];
    
    private readonly Dictionary<int, int> _beams = new();

    private readonly List<BeamEnd> _beamEnds = [];

    private char[,] _map;
    
    private Color[] _palette;

    private long _frame;
    
    private Texture2D _spark;
    
    private Texture2D _dish;

    private readonly List<Spark> _sparks = [];

    private readonly Random _rng = new();

    private int _part;

    private int _chunkSize = 50;

    private bool _done;

    private (int X, int Y, char Direction) _lastLaser = (0, 0, '\0');

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 812,
                                     PreferredBackBufferHeight = 812
                                 };

        Content.RootDirectory = "./16";

        IgnoreQueueLimit = true;
    }

    public override void SetPart(int part)
    {
        switch (part)
        {
            case 1:
                _part = 1;
                Puzzle = new Part1(this);

                break;

            case 2:
                _part = 2;
                Puzzle = new Part2(this);

                break;

            default:
                throw new VisualisationParameterException();
        }
    }

    protected override void Initialize()
    {
        IsMouseVisible = true;

        _palette = PaletteGenerator.GetPalette(26,
        [
            new Color(46, 27, 134),
                new Color(119, 35, 172),
                new Color(176, 83, 203),
                new Color(255, 168, 76),
                new Color(254, 211, 56),
                new Color(254, 253, 0)
        ]);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tiles = Content.Load<Texture2D>("map-tiles");

        _spark = Content.Load<Texture2D>("spark");

        _dish = Content.Load<Texture2D>("dish");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _frame++;

        if (_part == 1)
        {
            UpdatePart1();
        }
        else
        {
            if (_done)
            {
                MoveLaser();
            }
            else
            {
                UpdatePart2();
            }
        }

        UpdateSparks();
        
        base.Update(gameTime);
    }

    private void MoveLaser()
    {
        if (_state.LaserY != -1 || _state.LaserX != 9)
        {
            for (var i = 0; i < 5; i++)
            {
                _sparks.Add(new Spark
                {
                    Position = new PointFloat { X = _state.LaserX * 7 + 26, Y = _state.LaserY * 7 + 26 },
                    Vector = new PointFloat { X = (-10f + _rng.Next(21)) / 10, Y = -_rng.Next(31) / 10f },
                    Ticks = 25,
                    StartTicks = 25,
                    SpriteOffset = _rng.Next(3) * 5
                });
            }
        }

        if (_state.LaserX == -1)
        {
            if (_state.LaserY < _map.GetLength(1))
            {
                _state.LaserY++;
                
                return;
            }

            _state.LaserY = _map.GetLength(1);
            _state.LaserX = 0;
            _state.StartDirection = 'N';
        }

        if (_state.LaserY == _map.GetLength(1))
        {
            if (_state.LaserX < _map.GetLength(0))
            {
                _state.LaserX++;
                
                return;
            }

            _state.LaserY = _map.GetLength(1) - 1;
            _state.LaserX = _map.GetLength(0);
            _state.StartDirection = 'W';
        }

        if (_state.LaserX == _map.GetLength(0))
        {
            if (_state.LaserY > 0)
            {
                _state.LaserY--;
                
                return;
            }

            _state.LaserY = -1;
            _state.LaserX = _map.GetLength(0) -1;
            _state.StartDirection = 'S';
        }

        if (_state.LaserY == -1)
        {
            // My puzzle input finds most energy at x: 9, y: -1. Hard coding viz for that for now.
            if (_state.LaserX > 9)
            {
                _state.LaserX--;
            }
            else
            {
                _segments = _bestSegments;
            }
        }
    }

    private void UpdatePart2()
    {
        if (HasNextState)
        {
            if (_state == null || (_state.Beams != null && _allBeams.Count == 0))
            {
                var state = GetNextState();

                if (state.Map != null && _map == null)
                {
                    _map = state.Map;
                }

                _state = state;

                if (state.LaserX != _lastLaser.X || state.LaserY != _lastLaser.Y)
                {
                    _segments.Clear();

                    TranslatePuzzleState();

                    _chunkSize = _state.Beams.Count < 50 ? 1 : _state.Beams.Count / 500;

                    _lastLaser = (state.LaserX, state.LaserY, state.StartDirection);
                }
            }
        }

        for (var i = 0; i < _beamEnds.Count; i++)
        {
            _beamEnds[i].Count--;
        }

        _beamEnds.RemoveAll(e => e.Count < 0);

        if (_frame < FastFrameStart)
        {
            foreach (var end in _beamEnds)
            {
                _sparks.Add(new Spark
                {
                    Position = new PointFloat { X = end.X * 7 + 26, Y = end.Y * 7 + 26 },
                    Vector = new PointFloat { X = (-10f + _rng.Next(21)) / 10, Y = -_rng.Next(31) / 10f },
                    Ticks = 20,
                    StartTicks = 20,
                    SpriteOffset = _rng.Next(3) * 5
                });
            }
        }

        if (_frame <= FastFrameStart || _done)
        {
            for (var i = 0; i < _chunkSize; i++)
            {
                CreateSegments();
            }
        }
        else
        {
            while (CreateSegments())
            {
            }
        }

        var modulo = _frame > FastFrameStart ? 20 : 50;
        
        if (_beams.Count == 0 && _frame % modulo == 0)
        {
            // My puzzle input finds most energy at x: 9, y: -1. Hard coding viz for that for now.
            if (_state.LaserX == 9 && _state.LaserY == -1)
            {
                _segments.ForEach(s => _bestSegments.Add(s));
            }
            
            if (_state.LaserX != -1 || _state.LaserY != 0)
            {
                _state = null;
            }
            else
            {
                _done = true;
                
                _segments.Clear();
            }

            _allBeams.Clear();
        }
    }

    private bool CreateSegments()
    {
        var remove = new List<int>();

        var add = new List<int>();

        var added = false;
        
        foreach (var beam in _beams)
        {
            var segment = _allBeams[beam.Key][beam.Value];

            float colorIndex;

            if (_frame > FastFrameStart && ! _done)
            {
                colorIndex = 20;
            }
            else
            {
                colorIndex = 25 - (int) (26f / _allBeams[beam.Key].Count * _beams[beam.Key]);
            }
            
            _segments.Add(new Segment { X = segment.X, Y = segment.Y, Tile = segment.Tile, ColorIndex = colorIndex });

            added = true;

            _beams[beam.Key]++;

            if (_beams[beam.Key] >= _allBeams[beam.Key].Count)
            {
                remove.Add(beam.Key);

                var found = false;

                foreach (var newBeam in _allBeams.Where(b => b.Value[0].SourceId == beam.Key))
                {
                    add.Add(newBeam.Key);

                    found = true;
                }

                if (! found)
                {
                    _beamEnds.Add(new BeamEnd { X = segment.X, Y = segment.Y, Count = _palette.Length - 1 });
                }
            }
        }

        foreach (var item in remove)
        {
            _beams.Remove(item);
        }

        foreach (var item in add)
        {
            _beams.Add(item, 0);
        }

        return added;
    }

    private void UpdatePart1()
    {
        if (HasNextState)
        {
            _state = GetNextState();

            if (_state.Map != null && _map == null)
            {
                _map = _state.Map;
            }
        }

        if (_state.Beams != null && _allBeams.Count == 0)
        {
            TranslatePuzzleState();
        }

        if (_frame % 2 == 0)
        {
            return;
        }
        
        UpdatePuzzleState();
    }

    private void UpdatePuzzleState()
    {
        if (_frame % 5 == 0)
        {
            for (var i = 0; i < _beamEnds.Count; i++)
            {
                _beamEnds[i].Count--;
            }

            _beamEnds.RemoveAll(e => e.Count < 0);
        }

        for (var i = 0; i < _segments.Count; i++)
        {
            if (_segments[i].ColorIndex > 0)
            {
                _segments[i].ColorIndex -= 0.2f;
            }
        }

        foreach (var end in _beamEnds)
        {
            _sparks.Add(new Spark
            {
                Position = new PointFloat { X = end.X * 7 + 26, Y = end.Y * 7 + 26},
                Vector = new PointFloat { X = (-10f + _rng.Next(21)) / 10, Y = -_rng.Next(31) / 10f },
                Ticks = 25,
                StartTicks = 25,
                SpriteOffset = _rng.Next(3) * 5
            });
        }
        
        var remove = new List<int>();
        
        var add = new List<int>();
        
        foreach (var beam in _beams)
        {
            var segment = _allBeams[beam.Key][beam.Value];
            
            _segments.Add(new Segment { X = segment.X, Y = segment.Y, Tile = segment.Tile, ColorIndex = _palette.Length - 1 });

            _beams[beam.Key]++;

            if (_beams[beam.Key] >= _allBeams[beam.Key].Count)
            {
                remove.Add(beam.Key);

                var found = false;
                
                foreach (var newBeam in _allBeams.Where(b => b.Value[0].SourceId == beam.Key))
                {
                    add.Add(newBeam.Key);

                    found = true;
                }

                if (! found)
                {
                    _beamEnds.Add(new BeamEnd { X = segment.X, Y = segment.Y, Count = _palette.Length - 1 });
                }
            }
        }

        foreach (var item in remove)
        {
            _beams.Remove(item);
        }

        foreach (var item in add)
        {
            _beams.Add(item, 0);
        }
    }

    private void TranslatePuzzleState()
    {
        var firstBeamId = -1;
        
        foreach (var beam in _state.Beams[1..])
        {
            if (! _allBeams.ContainsKey(beam.Id))
            {
                if (firstBeamId == -1)
                {
                    firstBeamId = beam.Id;
                }

                _allBeams.Add(beam.Id, []);
            }

            var currentBeam = _allBeams[beam.Id];

            var previous = _state.StartDirection;

            if (currentBeam.Count == 0)
            {
                if (_allBeams.ContainsKey(beam.SourceId) && _allBeams[beam.SourceId].Count > 0)
                {
                    previous = _allBeams[beam.SourceId].Last().Direction;
                        
                    currentBeam.Add((beam.X, beam.Y, beam.Direction, GetTile(previous, beam.Direction), beam.SourceId));
                        
                    continue;
                }
            }
            else
            {
                previous = currentBeam.Last().Direction;
            }

            currentBeam.Add((beam.X, beam.Y, beam.Direction, GetTile(previous, beam.Direction), beam.SourceId));
        }
            
        _beams.Add(firstBeamId, 0);
    }

    private static char GetTile(char d1, char d2)
    {
        return (d1, d2) switch
        {
            ('E', 'E') => '-',
            ('W', 'W') => '-',
            ('N', 'N') => '|',
            ('S', 'S') => '|',
            ('E', 'S') => '7',
            ('N', 'W') => '7',
            ('S', 'E') => 'L',
            ('W', 'N') => 'L',
            ('E', 'N') => 'J',
            ('S', 'W') => 'J',
            ('N', 'E') => 'F',
            ('W', 'S') => 'F',
            (_, _) => ' '
        };
    }

    private void UpdateSparks()
    {
        var toRemove = new List<Spark>();

        foreach (var spark in _sparks)
        {
            spark.Ticks--;

            if (spark.Ticks < 0)
            {
                toRemove.Add(spark);

                continue;
            }

            spark.Position.X += spark.Vector.X;

            spark.Position.Y += spark.Vector.Y;

            spark.Vector.Y += spark.YGravity;
        }

        foreach (var spark in toRemove)
        {
            _sparks.Remove(spark);
        }
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        DrawMap();

        DrawDish();
        
        DrawBeams();
        
        DrawSparks();
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawDish()
    {
        var laser = _lastLaser;

        if (_done || (_state != null && _state.StartDirection == '\0'))
        {
            laser = (_state.LaserX, _state.LaserY, _state.StartDirection);
        }

        if (laser.Direction == 'S')
        {
            _spriteBatch.Draw(_dish, new Vector2(15 + laser.X * 7, 0), new Rectangle(0, 0, 22, 22), Color.DarkMagenta, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }

        if (laser.Direction == 'N')
        {
            _spriteBatch.Draw(_dish, new Vector2(15 + laser.X * 7, 790), new Rectangle(0, 0, 22, 22), Color.DarkMagenta, 0, Vector2.Zero, Vector2.One, SpriteEffects.FlipVertically, 0);
        }

        if (laser.Direction == 'E')
        {
            _spriteBatch.Draw(_dish, new Vector2(0, 15 + laser.Y * 7), new Rectangle(22, 0, 22, 22), Color.DarkMagenta, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }

        if (laser.Direction == 'W')
        {
            _spriteBatch.Draw(_dish, new Vector2(790, 15 + laser.Y * 7), new Rectangle(22, 0, 22, 22), Color.DarkMagenta, 0, Vector2.Zero, Vector2.One, SpriteEffects.FlipHorizontally, 0);
        }
    }

    private void DrawBeams()
    {
        foreach (var segment in _segments)
        {
            var z = segment.ColorIndex / 100f + .1f;

            var color = _palette[(int) segment.ColorIndex];
            
            switch (segment.Tile)
            {
                case '|':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(7, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;
                    
                case '-':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(0, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;
                
                case 'L':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(28, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;
                    
                case '7':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(35, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;

                case 'F':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(42, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;
                    
                case 'J':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(49, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;
            }
        }
    }
    
    private void DrawSparks()
    {
        foreach (var spark in _sparks)
        {
            _spriteBatch.Draw(_spark, new Vector2(spark.Position.X, spark.Position.Y), new Rectangle(spark.SpriteOffset, 0, 5, 5), Color.White * ((float) spark.Ticks / spark.StartTicks), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }

    private void DrawMap()
    {
        if (_map == null)
        {
            return;
        }

        var color = Color.DarkCyan;

        for (var y = 0; y < _map.GetLength(1); y++)
        {
            for (var x = 0; x < _map.GetLength(0); x++)
            {
                switch (_map[x, y])
                {
                    case '\\':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(14, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .9f);
                        break;
                    
                    case '/':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(21, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .9f);
                        break;
                    
                    case '|':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(7, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                        break;
                    
                    case '-':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(0, 0, 7, 7), color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                        break;
                }
            }
        }
    }
}