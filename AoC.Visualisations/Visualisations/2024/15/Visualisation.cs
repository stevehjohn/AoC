using AoC.Solutions.Solutions._2024._15;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2024._15;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int Width = 100;

    private const int Height = 50;

    private const int Scale = 16;

    private readonly Queue<PuzzleState> _stateQueue = [];

    private readonly Color[] _data = new Color[Width * Height];

    private Texture2D _texture;

    private SpriteBatch _spriteBatch;
    
    private PuzzleState _state;
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = Width * Scale,
            PreferredBackBufferHeight = Height * Scale
        };

        Content.RootDirectory = "./15";

        IgnoreQueueLimit = true;
    }
    
    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            1 => new Part1(this),
            2 => new Part2(this),
            _ => throw new VisualisationParameterException()
        };

        if (part == 1)
        {
            GraphicsDeviceManager.PreferredBackBufferWidth /= 2;
        }
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
            _state = _stateQueue.Dequeue();
        }

        if (_state == null)
        {
            return;
        }

        for (var y = 0; y < _state.Height; y++)
        {
            for (var x = 0; x < _state.Width; x++)
            {
                var pixel = y * Width + x;

                var item = _state.Map[x, y];

                switch (item)
                {
                    case '#':
                        _data[pixel] = Color.FromNonPremultiplied(128, 128, 128, 255);
                        break;
                    
                    case '@':
                        _data[pixel] = Color.Black;
                        break;
                    
                    case 'O':
                    case '[':
                    case ']':
                        _data[pixel] = Color.FromNonPremultiplied(255, 64, 192, 255);
                        break;
                    
                    default:
                        if (_data[pixel].R > 4)
                        {
                            _data[pixel].R -= 4;
                            _data[pixel].G -= 4;
                        }

                        break;
                }
            }
        }

        _texture.SetData(_data);
        
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        _spriteBatch.Draw(_texture, 
            new Rectangle(0, 0, Width * Scale, Height * Scale), 
            new Rectangle(0, 0, Width, Height), Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}