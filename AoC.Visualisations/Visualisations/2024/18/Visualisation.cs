using AoC.Solutions.Solutions._2024._18;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2024._18;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int TileSize = 12; 

    private readonly Color[] _data = new Color[PuzzleState.Size * TileSize * PuzzleState.Size * TileSize];

    private readonly Queue<PuzzleState> _stateQueue = [];

    private PuzzleState _state;

    private Texture2D _texture;

    private SpriteBatch _spriteBatch;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = PuzzleState.Size * TileSize,
            PreferredBackBufferHeight = PuzzleState.Size * TileSize
        };

        Content.RootDirectory = "./15";
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

        _texture = new Texture2D(GraphicsDeviceManager.GraphicsDevice, PuzzleState.Size * TileSize, PuzzleState.Size * TileSize);

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
        
        _texture.SetData(_data);
        
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        _spriteBatch.Draw(_texture, 
            new Rectangle(0, 0, PuzzleState.Size * TileSize, PuzzleState.Size * TileSize), 
            new Rectangle(0, 0, PuzzleState.Size * TileSize, PuzzleState.Size * TileSize), Color.White);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}