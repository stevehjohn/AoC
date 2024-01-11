using AoC.Games.Games.Deflectors.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Games.Games.Deflectors.Actors;

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

    private int _beamStrength;

    private int _beamMaxSteps;

    public int BeamStrength => _beamStrength;
    
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
        SimulateBeams(spriteBatch);
    }
    
    private void SimulateBeams(SpriteBatch spriteBatch)
    {
        _hitEnds.Clear();

        _hitMirrors.Clear();

        _beamStrength = 0;

        _hitUnplaced = false;

        var beamSteps = 0;

        _beamMaxSteps += 2;

        foreach (var start in _arenaManager.Level.Starts)
        {
            beamSteps += SimulateBeam(spriteBatch, start, beamSteps);
        }

        while (_splitters.TryDequeue(out var splitter))
        {
            SimulateBeam(spriteBatch, new Start { X = splitter.X, Y = splitter.Y, Direction = splitter.Direction }, splitter.BeamSteps, splitter.Color, splitter.ColorDirection);
        }

        IsComplete = _hitEnds.Count == _arenaManager.Level.Ends.Length && State == State.Playing && ! _hitUnplaced;

        _paletteStart += _paletteDirection;

        if (_paletteStart == -1 || _paletteStart == _palette.Length)
        {
            _paletteDirection = -_paletteDirection;

            _paletteStart += _paletteDirection;
        }
    }

    private int SimulateBeam(SpriteBatch spriteBatch, Start start, int beamSteps, int? colorIndex = null, int? colorDirection = null)
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
            _beamStrength++;

            beamSteps++;

            if (beamSteps > _beamMaxSteps)
            {
                break;
            }

            if ((x - BeamFactor / 2) % BeamFactor == 0 && (y - BeamFactor / 2) % BeamFactor == 0)
            {
                if (IsBlocked(x, y) || IsEnd(x, y, dX, dY))
                {
                    break;
                }

                var result = HitsMirror(spriteBatch, x, y, dX, dY, beamSteps, colorIndex.Value, colorDirection.Value);

                if (result.ShouldBreak)
                {
                    break;
                }

                if (result.Mirror != '\0')
                {
                    switch (result.Mirror)
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

            DrawBeam(spriteBatch, x, y, dX, dY, oldDx, oldDy, colorIndex.Value);

            oldDx = dX;
            oldDy = dY;

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

    private (char Mirror, bool ShouldBreak) HitsMirror(SpriteBatch spriteBatch, int x, int y, int dX, int dY, int beamSteps, int colorIndex, int colorDirection)
    {
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
                    _beamMaxSteps = beamSteps;
                }
            }
        }

        if (mirror == '|' || mirror == '-')
        {
            if (mirror == '|' && dX != 0)
            {
                _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.North, beamSteps, colorIndex, colorDirection));
                _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.South, beamSteps, colorIndex, colorDirection));

                spriteBatch.Draw(_beams,
                    new Vector2(x * BeamSize, Constants.TopOffset + y * BeamSize),
                    new Rectangle(dX == 1 ? 3 * BeamSize : 2 * BeamSize, 0, 7, 7), _palette[colorIndex],
                    0, Vector2.Zero, Vector2.One, SpriteEffects.None, .2f);

                return (mirror, true);
            }

            if (mirror == '-' && dY != 0)
            {
                _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.East, beamSteps, colorIndex, colorDirection));
                _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.West, beamSteps, colorIndex, colorDirection));

                spriteBatch.Draw(_beams,
                    new Vector2(x * BeamSize, Constants.TopOffset + y * BeamSize),
                    new Rectangle(dY == 1 ? 2 * BeamSize : 3 * BeamSize, 0, 7, 7), _palette[colorIndex],
                    0, Vector2.Zero, Vector2.One, SpriteEffects.None, .2f);

                return (mirror, true);
            }
        }

        return (mirror, false);
    }

    private bool IsEnd(int x, int y, int dX, int dY)
    {
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

            return true;
        }

        return false;
    }

    private bool IsBlocked(int x, int y)
    {
        var blocker = _arenaManager.Level.Blocked.SingleOrDefault(e => e.X == x / BeamFactor && e.Y == y / BeamFactor);

        if (blocker != null)
        {
            _sparkManager.Add(x * BeamSize, y * BeamSize, 10, 21, 30, 0.1f, Color.FromNonPremultiplied(0, 128, 255, 255));

            return true;
        }

        return false;
    }

    private void DrawBeam(SpriteBatch spriteBatch, int x, int y, int dX, int dY, int oldDx, int oldDy, int colorIndex)
    {
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

        spriteBatch.Draw(_beams,
            new Vector2(x * BeamSize, Constants.TopOffset + y * BeamSize),
            new Rectangle(beam * BeamSize, 0, 7, 7), _palette[colorIndex],
            0, Vector2.Zero, Vector2.One, SpriteEffects.None, .2f);
    }
}