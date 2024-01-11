using AoC.Games.Games.Deflectors.Levels;
using AoC.Games.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors;

public class BeamSimulator : IActor
{
    private const int BeamSize = 7;

    private const int BeamFactor = Constants.TileSize / BeamSize;
    
    private readonly SparkManager _sparkManager;
    
    private readonly ArenaManager _arenaManager;
    
    private Texture2D _beams;

    private readonly Color[] _palette;

    private int _paletteStart;

    private int _paletteDirection = -1;

    private readonly HashSet<(int, int)> _hitEnds = [];

    private readonly HashSet<(int, int)> _hitMirrors = [];
    
    private readonly Queue<(int X, int Y, Direction Direction, int BeamSteps, int Color, int ColorDirection)> _splitters = [];

    private bool _hitUnplaced;

    private int _beam;

    public int BeamMaxSteps { get; set; }

    public int BeamStrength => _beam;
    
    public State State { private get; set; }
    
    public bool IsComplete { get; private set; }

    public bool HitAllMirrors => _hitMirrors.Count == _arenaManager.Level.Mirrors.Count;

    public bool HitAllEnds => _hitEnds.Count == _arenaManager.Level.Ends.Length;

    public BeamSimulator(SparkManager sparkManager, ArenaManager arenaManager)
    {
        _palette = PaletteGenerator.GetPalette(26,
        [
            new Color(46, 27, 134),
            new Color(119, 35, 172),
            new Color(176, 83, 203),
            new Color(255, 168, 76),
            new Color(254, 211, 56),
            new Color(254, 253, 0)
        ]);

        _sparkManager = sparkManager;
        
        _arenaManager = arenaManager;
    }

    public void LoadContent(ContentManager contentManager)
    {
        _beams = contentManager.Load<Texture2D>("beams");
    }

    public void Update()
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawBeams(spriteBatch);
    }
    
    private void DrawBeams(SpriteBatch spriteBatch)
    {
        _hitEnds.Clear();

        _hitMirrors.Clear();

        _beam = 0;

        _hitUnplaced = false;

        var beamSteps = 0;

        BeamMaxSteps += 2;

        foreach (var start in _arenaManager.Level.Starts)
        {
            beamSteps += DrawBeam(spriteBatch, start, beamSteps);
        }

        while (_splitters.TryDequeue(out var splitter))
        {
            DrawBeam(spriteBatch, new Start { X = splitter.X, Y = splitter.Y, Direction = splitter.Direction }, splitter.BeamSteps, splitter.Color, splitter.ColorDirection);
        }

        IsComplete = _hitEnds.Count == _arenaManager.Level.Ends.Length && State == State.Playing && ! _hitUnplaced;

        _paletteStart += _paletteDirection;

        if (_paletteStart == -1 || _paletteStart == _palette.Length)
        {
            _paletteDirection = -_paletteDirection;

            _paletteStart += _paletteDirection;
        }
    }

    private int DrawBeam(SpriteBatch spriteBatch, Start start, int beamSteps, int? colorIndex = null, int? colorDirection = null)
    {
        var x = start.X * BeamFactor + BeamFactor / 2;

        var y = start.Y * BeamFactor + BeamFactor / 2;

        var dX = start.Direction switch
        {
            Direction.West => -1,
            Direction.East => 1,
            _ => 0
        };

        var dY = start.Direction switch
        {
            Direction.North => -1,
            Direction.South => 1,
            _ => 0
        };

        colorIndex ??= _paletteStart;
        colorDirection ??= -_paletteDirection;

        var oldDx = dX;
        var oldDy = dY;

        while (x >= 0 && x < Constants.MapSize * BeamFactor && y >= 0 && y < Constants.MapSize * BeamFactor)
        {
            _beam++;

            beamSteps++;

            if (beamSteps > BeamMaxSteps)
            {
                break;
            }

            if ((x - BeamFactor / 2) % BeamFactor == 0 && (y - BeamFactor / 2) % BeamFactor == 0)
            {
                var blocker = _arenaManager.Level.Blocked.SingleOrDefault(e => e.X == x / BeamFactor && e.Y == y / BeamFactor);

                if (blocker != null)
                {
                    _sparkManager.Add(x * BeamSize, y * BeamSize, 10, 21, 30, 0.1f, Color.FromNonPremultiplied(0, 128, 255, 255));

                    break;
                }

                var end = _arenaManager.Level.Ends.SingleOrDefault(e => e.X == x / BeamFactor && e.Y == y / BeamFactor);

                if (end != null)
                {
                    var valid = end.Direction switch
                    {
                        Direction.North => dY == 1,
                        Direction.South => dY == -1,
                        Direction.East => dX == -1,
                        Direction.West => dX == 1,
                        _ => false
                    };

                    if (valid)
                    {
                        if (! IsComplete)
                        {
                            _sparkManager.Add(x * BeamSize, y * BeamSize, 10, 41, 100, 0.1f, Color.FromNonPremultiplied(255, 0, 0, 255), Color.FromNonPremultiplied(255, 255, 0, 255));
                        }
                        else if (IsComplete)
                        {
                            _sparkManager.Add(x * BeamSize, y * BeamSize, 5, 21, 100, -0.01f, Color.FromNonPremultiplied(0, 255, 255, 255));
                        }

                        _hitEnds.Add((end.X, end.Y));
                    }

                    break;
                }

                var mirror = _arenaManager.Level.Mirrors.SingleOrDefault(m => m.X == x / BeamFactor && m.Y == y / BeamFactor)?.Piece ?? '\0';

                if (mirror != '\0')
                {
                    _hitMirrors.Add((x, y));
                }

                if (mirror == '\0')
                {
                    if (_arenaManager.MirrorPosition.X == x / BeamFactor && _arenaManager.MirrorPosition.Y == y / BeamFactor)
                    {
                        _hitUnplaced = true;

                        mirror = _arenaManager.Mirror;

                        if (_arenaManager.LastMirrorPosition != _arenaManager.MirrorPosition)
                        {
                            BeamMaxSteps = beamSteps;
                        }
                    }
                }

                if (mirror == '|' || mirror == '-')
                {
                    if (mirror == '|' && dX != 0)
                    {
                        _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.North, beamSteps, colorIndex.Value, colorDirection.Value));
                        _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.South, beamSteps, colorIndex.Value, colorDirection.Value));

                        spriteBatch.Draw(_beams,
                            new Vector2(x * BeamSize, Constants.TopOffset + y * BeamSize),
                            new Rectangle(dX == 1 ? 3 * BeamSize : 2 * BeamSize, 0, 7, 7), _palette[colorIndex.Value],
                            0, Vector2.Zero, Vector2.One, SpriteEffects.None, .2f);

                        break;
                    }

                    if (mirror == '-' && dY != 0)
                    {
                        _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.East, beamSteps, colorIndex.Value, colorDirection.Value));
                        _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.West, beamSteps, colorIndex.Value, colorDirection.Value));

                        spriteBatch.Draw(_beams,
                            new Vector2(x * BeamSize, Constants.TopOffset + y * BeamSize),
                            new Rectangle(dY == 1 ? 2 * BeamSize : 3 * BeamSize, 0, 7, 7), _palette[colorIndex.Value],
                            0, Vector2.Zero, Vector2.One, SpriteEffects.None, .2f);

                        break;
                    }
                }

                if (mirror != '\0')
                {
                    switch (mirror)
                    {
                        case '\\':
                            (dX, dY) = (dY, dX);

                            break;

                        case '/':
                            (dX, dY) = (-dY, -dX);

                            break;
                    }
                }
            }

            int beam = 0;

            if (oldDx == dX && oldDy == dY)
            {
                beam = dX == 0 ? 1 : 0;
            }
            else
            {
                switch (oldDx)
                {
                    case 1:
                        beam = dY == 1 ? 3 : 5;
                        break;

                    case -1:
                        beam = dY == 1 ? 4 : 2;
                        break;

                    default:
                        switch (oldDy)
                        {
                            case 1:
                                beam = dX == 1 ? 2 : 5;
                                break;

                            case -1:
                                beam = dX == 1 ? 4 : 3;
                                break;
                        }

                        break;
                }
            }

            oldDx = dX;
            oldDy = dY;

            spriteBatch.Draw(_beams,
                new Vector2(x * BeamSize, Constants.TopOffset + y * BeamSize),
                new Rectangle(beam * BeamSize, 0, 7, 7), _palette[colorIndex.Value],
                0, Vector2.Zero, Vector2.One, SpriteEffects.None, .2f);

            colorIndex += colorDirection;

            if (colorIndex == -1 || colorIndex == _palette.Length)
            {
                colorDirection = -colorDirection;

                colorIndex += colorDirection;
            }

            x += dX;
            y += dY;
        }

        return beamSteps;
    }
}