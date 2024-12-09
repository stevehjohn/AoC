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
    private const int GridSize = 308;

    private const int Scale = 3;

    private readonly Queue<PuzzleState> _stateQueue = [];

    private readonly Color[] _data = new Color[GridSize * GridSize];

    private Texture2D _texture;

    private SpriteBatch _spriteBatch;

    private PuzzleState _state;
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = GridSize * Scale,
            PreferredBackBufferHeight = GridSize * Scale
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

        _texture = new Texture2D(GraphicsDeviceManager.GraphicsDevice, GridSize, GridSize);

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
        if (_stateQueue.Count > 0)
        {
            _state = _stateQueue.Dequeue();
        }
        
        for (var y = 0; y < GridSize; y++)
        {
            for (var x = 0; x < GridSize; x++)
            {
                var part = y * GridSize + x;

                if (part >= _state.Length)
                {
                    _data[part] = Color.Black;
                    
                    continue;
                }

                if (_state[part] != -1)
                {
                    _data[part] = Color.Aqua;
                    
                    continue;
                }

                _data[part] = Color.Black;
            }
        }

        _texture.SetData(_data);
        
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        _spriteBatch.Draw(_texture, 
            new Rectangle(0, 0, GridSize * Scale, GridSize * Scale), 
            new Rectangle(0, 0, GridSize, GridSize), Color.White);

        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}