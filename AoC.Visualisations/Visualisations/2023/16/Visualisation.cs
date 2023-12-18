﻿using AoC.Solutions.Solutions._2023._16;
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
    private SpriteBatch _spriteBatch;
    
    private Texture2D _tiles;

    private PuzzleState _state;

    private readonly Dictionary<int, List<(int X, int Y, char Direction, char Tile, int SourceId)>> _allBeams = new();

    private readonly List<Segment> _segments = new();
    
    private readonly Dictionary<int, int> _beams = new();

    private readonly List<BeamEnd> _beamEnds = new();
    
    private Color[] _palette;

    private long _frame;
    
    private Texture2D _spark;

    private readonly List<Spark> _sparks = new();

    private readonly Random _rng = new();

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
                Puzzle = new Part1(this);

                break;
            case 2:
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
            new[]
            {
                new Color(46, 27, 134),
                new Color(119, 35, 172),
                new Color(176, 83, 203),
                new Color(255, 168, 76),
                new Color(254, 211, 56),
                new Color(254, 253, 0)
            });

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tiles = Content.Load<Texture2D>("map-tiles");

        _spark = Content.Load<Texture2D>("spark");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _frame++;
        
        if (HasNextState)
        {
            _state = GetNextState();
        }

        if (_state.Beams != null && _allBeams.Count == 0)
        {
            foreach (var beam in _state.Beams[1..])
            {
                if (! _allBeams.ContainsKey(beam.Id))
                { 
                    _allBeams.Add(beam.Id, new List<(int X, int Y, char Direction, char Tile, int SourceId)>());
                }

                var currentBeam = _allBeams[beam.Id];

                var previous = 'E';

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
            
            _beams.Add(1, 0);
        }

        if (_frame % 2 == 0)
        {
            return;
        }

        if (_frame % 5 == 0)
        {
            for (var i = 0; i < _segments.Count; i++)
            {
                _segments[i].ColorIndex--;
            }

            _segments.RemoveAll(s => s.ColorIndex < 0);

            for (var i = 0; i < _beamEnds.Count; i++)
            {
                _beamEnds[i].Count--;
            }

            _beamEnds.RemoveAll(e => e.Count < 0);
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

        UpdateSparks();
        
        base.Update(gameTime);
    }

    private char GetTile(char d1, char d2)
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

        DrawBeams();
        
        DrawSparks();
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawBeams()
    {
        foreach (var segment in _segments)
        {
            var z = segment.ColorIndex / 100f + .1f;
            
            switch (segment.Tile)
            {
                case '|':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(7, 0, 7, 7), _palette[segment.ColorIndex], 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;
                    
                case '-':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(0, 0, 7, 7), _palette[segment.ColorIndex], 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;
                
                case 'L':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(28, 0, 7, 7), _palette[segment.ColorIndex], 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;
                    
                case '7':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(35, 0, 7, 7), _palette[segment.ColorIndex], 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;

                case 'F':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(42, 0, 7, 7), _palette[segment.ColorIndex], 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
                    break;
                    
                case 'J':
                    _spriteBatch.Draw(_tiles, new Vector2(22 + segment.X * 7, 22 + segment.Y * 7), new Rectangle(49, 0, 7, 7), _palette[segment.ColorIndex], 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
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
        var map = _state.Map;
        
        for (var y = 0; y < map.GetLength(1); y++)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                switch (map[x, y])
                {
                    case '\\':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(14, 0, 7, 7), Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .9f);
                        break;
                    
                    case '/':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(21, 0, 7, 7), Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .9f);
                        break;
                    
                    case '|':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(7, 0, 7, 7), Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                        break;
                    
                    case '-':
                        _spriteBatch.Draw(_tiles, new Vector2(22 + x * 7, 22 + y * 7), new Rectangle(0, 0, 7, 7), Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                        break;
                }
            }
        }
    }
}