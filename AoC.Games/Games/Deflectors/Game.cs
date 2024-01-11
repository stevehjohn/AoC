﻿using AoC.Games.Games.Deflectors.Levels;
using AoC.Games.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AoC.Games.Games.Deflectors;

public class Game : Microsoft.Xna.Framework.Game
{
    private readonly float _scaleFactor;

    private const int MapSize = 30;

    private const int TileSize = 21;

    private const int BeamSize = 7;

    private const int BeamFactor = TileSize / BeamSize;

    private const int TopOffset = 32;

    private const string HighScoreFile = "high-score.txt";

    private const int BufferWidth = 693;

    private const int BufferHeight = 663;

    private int _levelNumber = 1;

    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;

    private SpriteBatch _spriteBatch;

    private RenderTarget2D _renderTarget;

    private Texture2D _beams;

    private readonly List<IActor> _actors = [];
    
    private readonly SparkManager _sparkManager;

    private readonly ArenaManager _arenaManager;
    
    private SpriteFont _font;

    private readonly LevelDataProvider _levelDataProvider = new();

    private Level _level;

    private Color[] _palette;

    private int _paletteStart;

    private int _paletteDirection = -1;

    private char _mirror;

    private (int X, int Y) _mirrorPosition;

    private (int X, int Y) _lastMirrorPosition = (-1, -1);

    private readonly Input _input = new();
    
    private readonly Queue<(int X, int Y, Direction Direction, int BeamSteps, int Color, int ColorDirection)> _splitters = [];

    private State _state;

    private int _frame;

    private int _beam;

    private int _score;

    private int _displayScore;

    private int _beamMaxSteps;

    private string _message;

    private int _highScore;

    private bool _hitUnplaced;

    private readonly HashSet<(int, int)> _hitEnds = [];

    private readonly HashSet<(int, int)> _hitMirrors = [];
    
    public Game()
    {
        _scaleFactor = AppSettings.Instance.ScaleFactor;

        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = (int) (BufferWidth * _scaleFactor),
            PreferredBackBufferHeight = (int) (BufferHeight * _scaleFactor)
        };

        Content.RootDirectory = "./Deflectors";

        IsMouseVisible = true;

        _sparkManager = new SparkManager(TopOffset);
        
        _actors.Add(_sparkManager);
        
        _levelDataProvider.LoadLevels();

        _arenaManager = new ArenaManager(TopOffset, _levelDataProvider);
        
        _actors.Add(_arenaManager);
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

        Window.Title = "The Floor Will be Lava";

        base.Initialize();
    }

    private void LoadLevel()
    {
        _levelDataProvider.LoadLevels();

        _level = _levelDataProvider.GetLevel(_levelNumber);

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

        _renderTarget = new RenderTarget2D(GraphicsDevice, BufferWidth, BufferHeight);

        foreach (var actor in _actors)
        {
            actor.LoadContent(Content);
        }
        
        _beams = Content.Load<Texture2D>("beams");
        
        _font = Content.Load<SpriteFont>("font");
    }

    protected override void Update(GameTime gameTime)
    {
        if (AppSettings.Instance.AllowCheat)
        {
            if (_input.KeyPressed(Keys.R))
            {
                LoadLevel();

                _arenaManager.SetLevel(_levelNumber);
                
                _state = State.Playing;

                _message = null;
            }

            if (_input.KeyPressed(Keys.N))
            {
                _levelNumber++;

                if (_levelNumber > _levelDataProvider.LevelCount)
                {
                    _levelNumber = 1;
                }

                _arenaManager.SetLevel(_levelNumber);

                LoadLevel();

                _state = State.Playing;

                _message = null;
            }
        }

        if (_state == State.Starting)
        {
            _state = State.Playing;
        }

        if (_state == State.AwaitingStart)
        {
            if (_input.LeftButtonClicked())
            {
                _state = State.Starting;

                _message = null;
            }
        }

        var position = (X: _input.MouseX / _scaleFactor, Y: _input.MouseY / _scaleFactor);

        if (position.X >= 0 && position.X < MapSize * TileSize && position.Y >= 0 && position.Y < MapSize * TileSize && _mirror != '\0')
        {
            IsMouseVisible = false;

            _lastMirrorPosition = _mirrorPosition;

            _mirrorPosition = ((int) (position.X / TileSize), (int) (position.Y / TileSize));
        }
        else
        {
            IsMouseVisible = true;

            _mirrorPosition = (-1, -1);
        }

        if (_message != null)
        {
            IsMouseVisible = false;
        }

        if (_state == State.Playing && _input.LeftButtonClicked())
        {
            if (position.X >= 0 && position.X < MapSize * TileSize && position.Y >= 0 && position.Y < MapSize * TileSize && _mirror != '\0')
            {
                PlaceMirror();
            }
        }

        if (_state == State.Failed)
        {
            if (_input.LeftButtonClicked())
            {
                _levelNumber = 1;

                _score = 0;

                _displayScore = 0;

                LoadLevel();
                
                _arenaManager.SetLevel(_levelNumber);

                _message = null;

                _state = State.Playing;
            }
        }

        if (_level.Pieces.Count == 0 && _mirror == '\0' && _state == State.Playing)
        {
            if (_hitEnds.Count < _level.Ends.Length && _beamMaxSteps >= 10_000_000)
            {
                _message = "OH DEAR,\nLOOKS LIKE YOU CAN'T\nCOMPLETE THIS LEVEL.\nCLICK TO RESTART.";

                _state = State.Failed;
            }

            _beamMaxSteps = 10_000_000;
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

                var mirrors = _hitMirrors.Count < _level.Mirrors.Count || _mirror != '\0'
                    ? "YOU COULD HAVE OBTAINED\nA HIGHER SCORE IF YOU\nHIT ALL YOUR MIRRORS.\n"
                    : string.Empty;

                if (_levelNumber < _levelDataProvider.LevelCount)
                {
                    _message = $"LEVEL COMPLETE.\n{mirrors}CLICK FOR NEXT LEVEL...";
                }
                else
                {
                    if (_score > _highScore)
                    {
                        _message = $"ALL LEVELS COMPLETE.\nCONGRATULATIONS!\nNEW HIGH SCORE!\n{mirrors}CLICK TO PLAY AGAIN...";
                    }
                    else
                    {
                        _message = $"ALL LEVELS COMPLETE.\n{mirrors}CLICK TO PLAY AGAIN...";
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

            if (_input.LeftButtonClicked())
            {
                if (_score >= _highScore)
                {
                    _highScore = _score;

                    File.WriteAllText(HighScoreFile, _highScore.ToString());
                }

                if (_levelNumber < _levelDataProvider.LevelCount)
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
                
                _arenaManager.SetLevel(_levelNumber);

                _message = null;

                _state = State.Playing;
            }
        }

        foreach (var actor in _actors)
        {
            actor.Update();
        }

        _arenaManager.MirrorPosition = _mirrorPosition;

        _arenaManager.Mirror = _mirror;
        
        _input.UpdateState();
        
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


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);

        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack);

        DrawBeams();

        foreach (var actor in _actors)
        {
            actor.Draw(_spriteBatch);
        }
        
        DrawInfo();

        DrawMessage();

        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp);

        _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, (int) (BufferWidth * _scaleFactor), (int) (BufferHeight * _scaleFactor)), Color.White);

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
                _spriteBatch.DrawString(_font, _message, new Vector2(start + x, 200 + y), Color.Black, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .6f);
            }
        }

        _spriteBatch.DrawString(_font, _message, new Vector2(start, 200), Color.FromNonPremultiplied(255, 255, 255, 255), 0, Vector2.Zero, Vector2.One,
            SpriteEffects.None, .7f);
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
        _hitEnds.Clear();

        _hitMirrors.Clear();

        _beam = 0;

        _hitUnplaced = false;

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

        if (_hitEnds.Count == _level.Ends.Length && _state == State.Playing && ! _hitUnplaced)
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
                    _sparkManager.Add(x * BeamSize, y * BeamSize, 10, 21, 30, 0.1f, Color.FromNonPremultiplied(0, 128, 255, 255));

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
                            _sparkManager.Add(x * BeamSize, y * BeamSize, 10, 41, 100, 0.1f, Color.FromNonPremultiplied(255, 0, 0, 255), Color.FromNonPremultiplied(255, 255, 0, 255));
                        }
                        else if (_state == State.LevelComplete)
                        {
                            _sparkManager.Add(x * BeamSize, y * BeamSize, 5, 21, 100, -0.01f, Color.FromNonPremultiplied(0, 255, 255, 255));
                        }

                        _hitEnds.Add((end.X, end.Y));
                    }

                    break;
                }

                var mirror = _level.Mirrors.SingleOrDefault(m => m.X == x / BeamFactor && m.Y == y / BeamFactor)?.Piece ?? '\0';

                if (mirror != '\0')
                {
                    _hitMirrors.Add((x, y));
                }

                if (mirror == '\0')
                {
                    if (_mirrorPosition.X == x / BeamFactor && _mirrorPosition.Y == y / BeamFactor)
                    {
                        _hitUnplaced = true;

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
}