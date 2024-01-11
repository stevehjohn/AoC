using AoC.Games.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AoC.Games.Games.Deflectors;

public class Game : Microsoft.Xna.Framework.Game
{
    private readonly float _scaleFactor;

    private const string HighScoreFile = "high-score.txt";

    private const int BufferWidth = 693;

    private const int BufferHeight = 663;

    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;

    private SpriteBatch _spriteBatch;

    private RenderTarget2D _renderTarget;

    private readonly List<IActor> _actors = [];

    private readonly ArenaManager _arenaManager;

    private readonly TextManager _textManager;

    private readonly BeamSimulator _beamSimulator;
    
    private readonly Input _input = new();

    private State _state;

    private int _frame;

    private int _score;

    private int _highScore;
    
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
        _textManager.Message = "WELCOME TO THE FLOOR WILL BE LAVA.\nINSPIRED BY ERIC WASTL'S\nADVENT OF CODE.\nCLICK TO PLAY.";

        if (File.Exists(HighScoreFile))
        {
            var text = File.ReadAllText(HighScoreFile);

            _highScore = int.Parse(text);
        }

        Window.Title = "The Floor Will be Lava";

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _renderTarget = new RenderTarget2D(GraphicsDevice, BufferWidth, BufferHeight);

        foreach (var actor in _actors)
        {
            actor.LoadContent(Content);
        }
    }

    protected override void Update(GameTime gameTime)
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

        if (_state == State.Starting)
        {
            _state = State.Playing;
        }

        if (_state == State.AwaitingStart)
        {
            if (_input.LeftButtonClicked())
            {
                _state = State.Starting;

                _textManager.Message = null;
            }
        }

        IsMouseVisible = _textManager.Message == null && _arenaManager.MirrorPosition == (-1, -1);
        
        if (_state == State.Playing && _input.LeftButtonClicked())
        {
            _arenaManager.PlaceMirror();
        }

        if (_state == State.Failed)
        {
            if (_input.LeftButtonClicked())
            {
                _score = 0;
                
                _arenaManager.SetLevel(1);

                _textManager.Message = null;

                _state = State.Playing;
            }
        }

        if (_arenaManager.Level.Pieces.Count == 0 && _arenaManager.Mirror == '\0' && _state == State.Playing)
        {
            if (! _beamSimulator.HitAllEnds && _beamSimulator.BeamMaxSteps >= 10_000_000)
            {
                _textManager.Message = "OH DEAR,\nLOOKS LIKE YOU CAN'T\nCOMPLETE THIS LEVEL.\nCLICK TO RESTART.";

                _state = State.Failed;
            }

            _beamSimulator.BeamMaxSteps = 10_000_000;
        }

        if (_beamSimulator.IsComplete && _state != State.PreparingNextLevel)
        {
            _frame++;

            if (_frame > 200)
            {
                _state = State.PreparingNextLevel;
                
                _score += _beamSimulator.BeamStrength / 3;

                var complete = _arenaManager.LevelNumber < _arenaManager.LevelCount
                    ? "LEVEL COMPLETE.\n"
                    : "ALL LEVELS COMPLETE\n";
                
                var mirrors = ! _beamSimulator.HitAllMirrors || _arenaManager.Mirror != '\0'
                    ? "YOU COULD HAVE OBTAINED\nA HIGHER SCORE IF YOU\nHIT ALL YOUR MIRRORS.\n"
                    : string.Empty;

                var highScore = _score > _highScore && _arenaManager.LevelNumber == _arenaManager.LevelCount
                    ? "CONGRATULATIONS!\nNEW HIGH SCORE!\n"
                    : string.Empty;

                _textManager.Message = $"{complete}{highScore}{mirrors}CLICK TO PLAY AGAIN...";

                if (highScore != string.Empty)
                {
                    _highScore = _score;

                    File.WriteAllText(HighScoreFile, _highScore.ToString());
                }

                _frame = 0;
            }
        }

        if (_state == State.PreparingNextLevel)
        {
            if (_input.LeftButtonClicked())
            {
                _arenaManager.NextLevel();

                _textManager.Message = null;

                _state = State.Playing;
            }
        }

        _beamSimulator.State = _state;

        _textManager.SetInformation(_arenaManager.LevelNumber, _beamSimulator.BeamStrength, _score, _highScore);
        
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

        _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, (int) (BufferWidth * _scaleFactor), (int) (BufferHeight * _scaleFactor)), Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}