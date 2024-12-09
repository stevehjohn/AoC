using AoC.Solutions.Solutions._2024._09;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2024._09;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int Width = 556;

    private const int Height = 170;

    private const int XScale = 3;

    private const int YScale = 6;

    private readonly Queue<PuzzleState> _stateQueue = [];

    private readonly Color[] _data = new Color[Width * Height];

    private Texture2D _texture;

    private SpriteBatch _spriteBatch;

    private PuzzleState _initialState;
    
    private PuzzleState _state;

    private PuzzleState _previousState;
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = Width * XScale,
            PreferredBackBufferHeight = Height * YScale
        };

        Content.RootDirectory = "./09";

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

        _texture = new Texture2D(GraphicsDeviceManager.GraphicsDevice, Width, Height);

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            _stateQueue.Enqueue(GetNextState());
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        if (_stateQueue.Count > 0)
        {
            _previousState = _state;
            
            _state = _stateQueue.Dequeue();

            _initialState ??= _state;
        }

        if (_previousState == null)
        {
            return;
        }

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var part = y * Width + x;

                if (part >= _state.Length)
                {
                    _data[part] = Color.Black;

                    continue;
                }

                if (_state[part] != -1)
                {
                    if (_initialState[part] != -1)
                    {
                        _data[part] = Color.FromNonPremultiplied(0, 0, 255, 255);
                        
                        continue;
                    }

                    if (_previousState[part] == -1)
                    {
                        _data[part] = Color.FromNonPremultiplied(255, 255, 255, 255);
                        
                        continue;
                    }

                    _data[part] = Color.FromNonPremultiplied(64, 64, 255, 255);
                    
                    continue;
                }

                if (_previousState[part] != -1)
                {
                    _data[part] = Color.FromNonPremultiplied(255, 0, 0, 255);
                
                    continue;
                }

                if (_initialState[part] != -1)
                {
                    _data[part] = Color.FromNonPremultiplied(0, 0, 128, 255);
                
                    continue;
                }

                _data[part] = Color.Black;
            }
        }

        _texture.SetData(_data);
        
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        _spriteBatch.Draw(_texture, 
            new Rectangle(0, 0, Width * XScale, Height * YScale), 
            new Rectangle(0, 0, Width, Height), Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}