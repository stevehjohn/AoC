using AoC.Games.Games.Deflectors.Actors;
using AoC.Games.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AoC.Games.Games.Deflectors;

public class Game : Microsoft.Xna.Framework.Game
{
    private readonly float _scaleFactor;

    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;

    private SpriteBatch _spriteBatch;

    private RenderTarget2D _renderTarget;

    private readonly Input _input = new();

    private readonly List<IActor> _actors = [];

    private readonly ArenaManager _arenaManager;

    private readonly TextManager _textManager;

    private readonly BeamSimulator _beamSimulator;
    
    private State _state;

    private int _frame;

    private int _score;
    
    public Game()
    {
        _scaleFactor = AppSettings.Instance.ScaleFactor;

        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = (int) (Constants.BufferWidth * _scaleFactor),
            PreferredBackBufferHeight = (int) (Constants.BufferHeight * _scaleFactor)
        };

        Content.RootDirectory = "./Deflectors";

        IsMouseVisible = true;

        var sparkManager = new SparkManager();
        
        _actors.Add(sparkManager);

        _arenaManager = new ArenaManager(_input);
        
        _actors.Add(_arenaManager);

        _textManager = new TextManager();
        
        _actors.Add(_textManager);

        _beamSimulator = new BeamSimulator(sparkManager, _arenaManager);
        
        _actors.Add(_beamSimulator);
    }

    protected override void Initialize()
    {        
        _textManager.Message = "WELCOME TO THE FLOOR WILL BE LAVA.\nINSPIRED BY ERIC WASTL'S\nADVENT OF CODE.\n\nCODE AND GRAPHICS BY STEVO JOHN\n\nCLICK TO PLAY.";

        Window.Title = "The Floor Will be Lava";

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _renderTarget = new RenderTarget2D(GraphicsDevice, Constants.BufferWidth, Constants.BufferHeight);

        foreach (var actor in _actors)
        {
            actor.LoadContent(Content);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        switch (_state)
        {
            case State.AwaitingStart:
                if (_input.LeftButtonClicked())
                {
                    _textManager.Message = null;

                    _frame = 0;

                    _score = 0;

                    _arenaManager.SetLevel(1);
                    
                    _state = State.Playing;
                }

                break;
            
            case State.Playing:
                CheckForCheat();
                
                if (_input.LeftButtonClicked())
                {
                    _arenaManager.PlaceMirror();
                }

                CheckForFailure();

                CheckComplete();

                break;
            
            case State.Failed:
                if (_input.LeftButtonClicked())
                {
                    _score = 0;
                
                    _arenaManager.SetLevel(1);

                    _textManager.Message = null;

                    _state = State.Playing;
                }
                
                break;
                
            case State.PreparingNextLevel:
                if (_input.LeftButtonClicked())
                {
                    _arenaManager.NextLevel();

                    _textManager.Message = null;

                    _state = State.Playing;
                }
                
                break;
        }

        IsMouseVisible = _textManager.Message == null && _arenaManager.MirrorPosition == (-1, -1);

        _beamSimulator.State = _state;

        _textManager.SetInformation(_arenaManager.LevelNumber, _beamSimulator.BeamStrength, _score, ScoreKeeper.GetHighScore());
        
        foreach (var actor in _actors)
        {
            actor.Update();
        }
        
        _input.UpdateState();
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);

        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack);

        foreach (var actor in _actors)
        {
            actor.Draw(_spriteBatch);
        }
        
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, blendState: BlendState.NonPremultiplied, samplerState: SamplerState.PointClamp);

        _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, (int) (Constants.BufferWidth * _scaleFactor), (int) (Constants.BufferHeight * _scaleFactor)), Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    private void CheckForCheat()
    {
        if (AppSettings.Instance.AllowCheat)
        {
            if (_input.KeyPressed(Keys.R))
            {
                _arenaManager.ResetLevel();
                
                _state = State.Playing;

                _textManager.Message = null;
            }

            if (_input.KeyPressed(Keys.N))
            {
                _arenaManager.NextLevel();

                _state = State.Playing;

                _textManager.Message = null;
            }
        }
    }

    private void CheckForFailure()
    {
        if (_arenaManager.Level.Pieces.Count == 0 && _arenaManager.Mirror == '\0')
        {
            if (! _beamSimulator.HitAllEnds)
            {
                _frame++;

                if (_frame > 200)
                {
                    _textManager.Message = $"OH DEAR,\nLOOKS LIKE YOU CAN'T\nCOMPLETE THIS LEVEL.\nCLICK TO RESTART.";

                    _state = State.Failed;

                    _frame = 0;
                }
            }
        }
    }

    private void CheckComplete()
    {
        if (_beamSimulator.IsComplete)
        {
            _frame++;

            if (_frame > 200)
            {
                _score += _beamSimulator.BeamStrength / 3;

                _state = _arenaManager.LevelNumber < _arenaManager.LevelCount ? State.PreparingNextLevel : State.AwaitingStart;
                
                var complete = _arenaManager.LevelNumber < _arenaManager.LevelCount
                    ? "LEVEL COMPLETE.\n"
                    : "ALL LEVELS COMPLETE\n";
                
                var mirrors = ! _beamSimulator.HitAllMirrors || _arenaManager.Mirror != '\0'
                    ? "YOU COULD HAVE OBTAINED\nA HIGHER SCORE IF YOU\nHIT ALL YOUR MIRRORS.\n"
                    : string.Empty;

                var highScore = ScoreKeeper.CheckHighScore(_score)
                    ? "CONGRATULATIONS!\nNEW HIGH SCORE!\n"
                    : string.Empty;
                
                var next = _arenaManager.LevelNumber < _arenaManager.LevelCount
                    ? "CLICK TO CONTINUE..."
                    : "CLICK TO PLAY AGAIN...";

                _textManager.Message = $"{complete}{highScore}{mirrors}{next}";

                _frame = 0;
            }
        }
    }
}