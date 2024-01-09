using AoC.Games.Games.Deflectors.Levels;
using AoC.Games.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AoC.Games.Games.Deflectors;

public class Game : Microsoft.Xna.Framework.Game
{
    private const int MapSize = 30;

    private const int TileSize = 21;

    private const int BeamSize = 7;

    private const int BeamFactor = TileSize / BeamSize;

    private const int TopOffset = 32;

    private const string HighScoreFile = "high-score.txt";

    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;

    private SpriteBatch _spriteBatch;

    private Texture2D _beams;

    private Texture2D _mirrors;

    private Texture2D _other;

    private Texture2D _spark;

    private SpriteFont _font;

    private readonly LevelDataProvider _levels = new();

    private int _levelNumber = 1;

    private Level _level;

    private Color[] _palette;

    private int _paletteStart;

    private int _paletteDirection = -1;

    private char _mirror;

    private (int X, int Y) _mirrorPosition;

    private (int X, int Y) _lastMirrorPosition;

    private bool _leftButtonPrevious;

    private readonly List<Spark> _sparks = [];

    private readonly Random _rng = new();

    private readonly Queue<(int X, int Y, Direction Direction, int BeamSteps, int Color, int ColorDirection)> _splitters = [];

    private int _endsHit;

    private State _state;

    private int _frame;

    private int _beam;

    private int _score;

    private int _displayScore;

    private int _beamMaxSteps;

    private string _message;

    private int _highScore;

    public Game()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 693,
            PreferredBackBufferHeight = 663
        };

        Content.RootDirectory = "./Deflectors";

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        LoadLevel();

        _message = "WELCOME TO THE FLOOR WILL BE LAVA.\nINSPIRED BY ERIC WASTL'S\nADVENT OF CODE.\nCLICK TO PLAY.";
        
        if (File.Exists(HighScoreFile))
        {
            var text = File.ReadAllText(HighScoreFile);

            _highScore = int.Parse(text);
        }

        base.Initialize();
    }

    private void LoadLevel()
    {
        _levels.LoadLevels();

        _level = _levels.GetLevel(_levelNumber);

        _palette = PaletteGenerator.GetPalette(26,
        [
            new Color(46, 27, 134),
            new Color(119, 35, 172),
            new Color(176, 83, 203),
            new Color(255, 168, 76),
            new Color(254, 211, 56),
            new Color(254, 253, 0)
        ]);

        _mirror = _level.Pieces[0];

        _level.Pieces.RemoveAt(0);

        _paletteStart = _palette.Length - 1;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _beams = Content.Load<Texture2D>("beams");

        _mirrors = Content.Load<Texture2D>("mirrors");

        _other = Content.Load<Texture2D>("other");

        _spark = Content.Load<Texture2D>("spark");

        _font = Content.Load<SpriteFont>("font");
    }

    protected override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        if (_state == State.AwaitingStart)
        {
            if (mouseState.LeftButton == ButtonState.Released && _leftButtonPrevious)
            {
                _state = State.Playing;

                _message = null;

                _leftButtonPrevious = false;
                
                return;
            }
        }

        var position = (mouseState.X, mouseState.Y);

        if (position.X >= 0 && position.X < MapSize * TileSize && position.Y >= 0 && position.Y < MapSize * TileSize && _mirror != '\0')
        {
            IsMouseVisible = false;

            _mirrorPosition = (position.X / TileSize, position.Y / TileSize);
        }
        else
        {
            IsMouseVisible = true;

            _mirrorPosition = (-1, -1);
        }

        if (_state == State.Playing && mouseState.LeftButton == ButtonState.Released && _leftButtonPrevious)
        {
            if (position.X >= 0 && position.X < MapSize * TileSize && position.Y >= 0 && position.Y < MapSize * TileSize && _mirror != '\0')
            {
                PlaceMirror();
            }
        }

        if (_state == State.LevelComplete)
        {
            _frame++;

            if (_displayScore < _score)
            {
                _displayScore++;
            }

            if (_frame > 200 && _displayScore == _score)
            {
                _state = State.PreparingNextLevel;

                if (_levelNumber < _levels.LevelCount)
                {
                    _message = "LEVEL COMPLETE.\nCLICK FOR NEXT LEVEL...";
                }
                else
                {
                    if (_score > _highScore)
                    {
                        _message = "ALL LEVELS COMPLETE.\nCONGRATULATIONS!\nNEW HIGH SCORE!\nCLICK TO PLAY AGAIN...";
                    }
                    else
                    {
                        _message = "ALL LEVELS COMPLETE.\nCLICK TO PLAY AGAIN...";
                    }
                }

                _frame = 0;
            }
        }

        if (_state == State.PreparingNextLevel)
        {
            if (_score > _highScore)
            {
                _highScore++;
            }

            if (mouseState.LeftButton == ButtonState.Released && _leftButtonPrevious)
            {
                if (_score >= _highScore)
                {
                    _highScore = _score;
                    
                    File.WriteAllText(HighScoreFile, _highScore.ToString());
                }

                if (_levelNumber < _levels.LevelCount)
                {
                    _levelNumber++;
                }
                else
                {
                    _levelNumber = 1;

                    _score = 0;

                    _displayScore = 0;
                }

                LoadLevel();

                _message = null;

                _state = State.Playing;
            }
        }

        _leftButtonPrevious = mouseState.LeftButton == ButtonState.Pressed;

        UpdateSparks();

        base.Update(gameTime);
    }

    private void PlaceMirror()
    {
        if (_level.Mirrors.Any(m => m.X == _mirrorPosition.X && m.Y == _mirrorPosition.Y)
            || _level.Blocked.Any(b => b.X == _mirrorPosition.X && b.Y == _mirrorPosition.Y)
            || _level.Starts.Any(s => s.X == _mirrorPosition.X && s.Y == _mirrorPosition.Y)
            || _level.Ends.Any(e => e.X == _mirrorPosition.X && e.Y == _mirrorPosition.Y))
        {
            return;
        }

        _level.Mirrors.Add(new Mirror
        {
            Piece = _mirror,
            Placed = true,
            X = _mirrorPosition.X,
            Y = _mirrorPosition.Y
        });

        _mirror = '\0';

        if (_level.Pieces.Count > 0)
        {
            _mirror = _level.Pieces[0];

            _level.Pieces.RemoveAt(0);
        }
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

        _spriteBatch.Begin(SpriteSortMode.FrontToBack);

        DrawBackground();

        DrawPieces();

        DrawStarts();

        DrawEnds();

        DrawMirrors();

        DrawBeams();

        DrawSparks();

        DrawInfo();

        DrawMessage();

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawMessage()
    {
        if (_message == null)
        {
            return;
        }

        var w = _font.MeasureString(_message).X;

        // ReSharper disable once PossibleLossOfFraction
        var start = TileSize * MapSize / 2 - w / 2;

        for (var y = -2; y < 3; y++)
        {
            for (var x = -2; x < 3; x++)
            {
                _spriteBatch.DrawString(_font, _message, new Vector2(start + x, 250 + y), Color.Black, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .5f);
            }
        }

        _spriteBatch.DrawString(_font, _message, new Vector2(start, 250), Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One,
            SpriteEffects.None, .6f);
    }

    private void DrawInfo()
    {
        _spriteBatch.DrawString(_font, "LEVEL:", new Vector2(10, -2), Color.FromNonPremultiplied(0, 128, 0, 255));

        var x = _font.MeasureString("LEVEL: ").X + 10;

        _spriteBatch.DrawString(_font, $"{_levelNumber,2}", new Vector2(x, -2), Color.FromNonPremultiplied(192, 192, 192, 255));

        _spriteBatch.DrawString(_font, "BEAM:", new Vector2(170, -2), Color.FromNonPremultiplied(0, 128, 0, 255));

        x = _font.MeasureString("BEAM: ").X + 170;

        _spriteBatch.DrawString(_font, $"{_beam / 3,3}", new Vector2(x, -2), Color.FromNonPremultiplied(192, 192, 192, 255));

        _spriteBatch.DrawString(_font, "SCORE:", new Vector2(340, -2), Color.FromNonPremultiplied(0, 128, 0, 255));

        x = _font.MeasureString("SCORE: ").X + 340;

        _spriteBatch.DrawString(_font, _displayScore.ToString(), new Vector2(x, -2), Color.FromNonPremultiplied(192, 192, 192, 255));

        _spriteBatch.DrawString(_font, "HI:", new Vector2(530, -2), Color.FromNonPremultiplied(0, 128, 0, 255));

        x = _font.MeasureString("HI: ").X + 530;

        _spriteBatch.DrawString(_font, _highScore.ToString(), new Vector2(x, -2), Color.FromNonPremultiplied(192, 192, 192, 255));
    }

    private void DrawBeams()
    {
        _endsHit = 0;

        _beam = 0;

        var beamSteps = 0;

        _beamMaxSteps += 2;

        foreach (var start in _level.Starts)
        {
            beamSteps += DrawBeam(start, beamSteps);
        }

        while (_splitters.TryDequeue(out var splitter))
        {
            DrawBeam(new Start { X = splitter.X, Y = splitter.Y, Direction = splitter.Direction }, splitter.BeamSteps, splitter.Color, splitter.ColorDirection);
        }

        if (_endsHit == _level.Ends.Length && _state == State.Playing && _mirror == '\0')
        {
            _state = State.LevelComplete;

            _score += _beam / 3;
        }

        _paletteStart += _paletteDirection;

        if (_paletteStart == -1 || _paletteStart == _palette.Length)
        {
            _paletteDirection = -_paletteDirection;

            _paletteStart += _paletteDirection;
        }
    }

    private int DrawBeam(Start start, int beamSteps, int? colorIndex = null, int? colorDirection = null)
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

        while (x >= 0 && x < MapSize * BeamFactor && y >= 0 && y < MapSize * BeamFactor)
        {
            _beam++;

            beamSteps++;

            if (beamSteps > _beamMaxSteps)
            {
                break;
            }

            if ((x - BeamFactor / 2) % BeamFactor == 0 && (y - BeamFactor / 2) % BeamFactor == 0)
            {
                var blocker = _level.Blocked.SingleOrDefault(e => e.X == x / BeamFactor && e.Y == y / BeamFactor);

                if (blocker != null)
                {
                    _sparks.Add(new Spark
                    {
                        Position = new PointFloat
                        {
                            X = x * BeamSize,
                            Y = y * BeamSize
                        },
                        Vector = new PointFloat
                            { X = (-10f + _rng.Next(21)) / 10, Y = -_rng.Next(21) / 10f },
                        Ticks = 30,
                        StartTicks = 30,
                        SpriteOffset = 0,
                        Color = Color.FromNonPremultiplied(0, 128, 255, 255)
                    });

                    break;
                }

                var end = _level.Ends.SingleOrDefault(e => e.X == x / BeamFactor && e.Y == y / BeamFactor);

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
                        if (_state == State.Playing)
                        {
                            _sparks.Add(new Spark
                            {
                                Position = new PointFloat
                                {
                                    X = x * BeamSize,
                                    Y = y * BeamSize
                                },
                                Vector = new PointFloat { X = (-10f + _rng.Next(21)) / 10, Y = -_rng.Next(41) / 10f },
                                Ticks = 100,
                                StartTicks = 100,
                                SpriteOffset = 0,
                                Color = _rng.Next(2) == 1 ? Color.FromNonPremultiplied(255, 0, 0, 255) : Color.FromNonPremultiplied(255, 255, 0, 255)
                            });
                        }
                        else if (_state == State.LevelComplete)
                        {
                            _sparks.Add(new Spark
                            {
                                Position = new PointFloat
                                {
                                    X = x * BeamSize,
                                    Y = y * BeamSize
                                },
                                Vector = new PointFloat { X = (-5f + _rng.Next(11)) / 10, Y = -_rng.Next(41) / 10f },
                                Ticks = 100,
                                StartTicks = 100,
                                SpriteOffset = 0,
                                YGravity = -0.01f,
                                Color = Color.FromNonPremultiplied(0, 255, 255, 255)
                            });
                        }

                        _endsHit++;
                    }

                    break;
                }

                var mirror = _level.Mirrors.SingleOrDefault(m => m.X == x / BeamFactor && m.Y == y / BeamFactor)?.Piece ?? '\0';

                if (mirror == '\0')
                {
                    if (_mirrorPosition.X == x / BeamFactor && _mirrorPosition.Y == y / BeamFactor)
                    {
                        mirror = _mirror;

                        if (_lastMirrorPosition != _mirrorPosition)
                        {
                            _beamMaxSteps = beamSteps;

                            _lastMirrorPosition = _mirrorPosition;
                        }
                    }
                }

                if (mirror == '|' || mirror == '-')
                {
                    if (mirror == '|' && dX != 0)
                    {
                        _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.North, beamSteps, colorIndex.Value, colorDirection.Value));
                        _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.South, beamSteps, colorIndex.Value, colorDirection.Value));

                        _spriteBatch.Draw(_beams,
                            new Vector2(x * BeamSize, TopOffset + y * BeamSize),
                            new Rectangle(dX == 1 ? 3 * BeamSize : 2 * BeamSize, 0, 7, 7), _palette[colorIndex.Value],
                            0, Vector2.Zero, Vector2.One, SpriteEffects.None, .2f);

                        break;
                    }

                    if (mirror == '-' && dY != 0)
                    {
                        _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.East, beamSteps, colorIndex.Value, colorDirection.Value));
                        _splitters.Enqueue((x / BeamFactor, y / BeamFactor, Direction.West, beamSteps, colorIndex.Value, colorDirection.Value));

                        _spriteBatch.Draw(_beams,
                            new Vector2(x * BeamSize, TopOffset + y * BeamSize),
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

            _spriteBatch.Draw(_beams,
                new Vector2(x * BeamSize, TopOffset + y * BeamSize),
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

    private void DrawPieces()
    {
        var index = 0;

        for (var y = 10; y > 0; y--)
        {
            if (index >= _level.Pieces.Count)
            {
                break;
            }

            var offset = _level.Pieces[index] switch
            {
                '|' => 1,
                '\\' => 2,
                '/' => 3,
                _ => 0
            };

            _spriteBatch.Draw(_mirrors,
                new Vector2(31 * TileSize, TopOffset + y * TileSize),
                new Rectangle(offset * TileSize, 0, TileSize, TileSize),
                Color.Cyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);

            index++;
        }
    }

    private void DrawMirrors()
    {
        foreach (var mirror in _level.Mirrors)
        {
            var offset = mirror.Piece switch
            {
                '|' => 1,
                '\\' => 2,
                '/' => 3,
                _ => 0
            };

            _spriteBatch.Draw(_mirrors,
                new Vector2(mirror.X * TileSize, TopOffset + mirror.Y * TileSize),
                new Rectangle(offset * TileSize, 0, TileSize, TileSize),
                mirror.Placed ? Color.Green : Color.DarkCyan, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .4f);
        }

        if (_mirrorPosition != (-1, -1))
        {
            var offset = _mirror switch
            {
                '|' => 1,
                '\\' => 2,
                '/' => 3,
                _ => 0
            };

            var color = Color.FromNonPremultiplied(0, 255, 0, 255);

            if (_level.Mirrors.Any(m => m.X == _mirrorPosition.X && m.Y == _mirrorPosition.Y)
                || _level.Blocked.Any(b => b.X == _mirrorPosition.X && b.Y == _mirrorPosition.Y)
                || _level.Starts.Any(s => s.X == _mirrorPosition.X && s.Y == _mirrorPosition.Y)
                || _level.Ends.Any(e => e.X == _mirrorPosition.X && e.Y == _mirrorPosition.Y))
            {
                color = Color.Red;
            }

            _spriteBatch.Draw(_mirrors,
                new Vector2(_mirrorPosition.X * TileSize, TopOffset + _mirrorPosition.Y * TileSize),
                new Rectangle(offset * TileSize, 0, TileSize, TileSize),
                color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .3f);
        }
    }

    private void DrawStarts()
    {
        foreach (var start in _level.Starts)
        {
            _spriteBatch.Draw(_other,
                new Vector2(start.X * TileSize, TopOffset + start.Y * TileSize),
                new Rectangle(0, 0, TileSize, TileSize),
                Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .1f);
        }
    }

    private void DrawEnds()
    {
        foreach (var end in _level.Ends)
        {
            var spriteX = end.Direction switch
            {
                Direction.North => 1,
                Direction.South => 1,
                _ => 2
            };

            var effect = end.Direction switch
            {
                Direction.South => SpriteEffects.FlipVertically,
                Direction.West => SpriteEffects.FlipHorizontally,
                _ => SpriteEffects.None
            };

            _spriteBatch.Draw(_other,
                new Vector2(end.X * TileSize, TopOffset + end.Y * TileSize),
                new Rectangle(spriteX * TileSize, 0, TileSize, TileSize),
                Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One, effect, .1f);
        }
    }

    private void DrawBackground()
    {
        for (var y = 0; y < MapSize; y++)
        {
            for (var x = 0; x < MapSize; x++)
            {
                if (! _level.Blocked.Any(b => b.X == x && b.Y == y))
                {
                    _spriteBatch.Draw(_other,
                        new Vector2(x * TileSize, TopOffset + y * TileSize),
                        new Rectangle(TileSize * 3, 0, TileSize, TileSize),
                        Color.FromNonPremultiplied(255, 255, 255, 25), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                }
                else
                {
                    _spriteBatch.Draw(_other,
                        new Vector2(x * TileSize, TopOffset + y * TileSize),
                        new Rectangle(TileSize * 3, 0, TileSize, TileSize),
                        Color.FromNonPremultiplied(255, 255, 255, 200), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                }
            }

            if (y > 0 && y <= 10)
            {
                _spriteBatch.Draw(_other,
                    new Vector2((MapSize + 1) * TileSize, TopOffset + y * TileSize),
                    new Rectangle(TileSize * 3, 0, TileSize, TileSize),
                    Color.FromNonPremultiplied(255, 255, 255, 25), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
            }
        }
    }

    private void DrawSparks()
    {
        foreach (var spark in _sparks)
        {
            _spriteBatch.Draw(_spark, new Vector2(spark.Position.X, TopOffset + spark.Position.Y),
                new Rectangle(spark.SpriteOffset, 0, 5, 5), spark.Color * ((float) spark.Ticks / spark.StartTicks), 0,
                Vector2.Zero, Vector2.One, SpriteEffects.None, 0.3f);
        }
    }
}