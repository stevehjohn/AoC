using AoC.Solutions.Solutions._2025._12;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2025._12;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int Width = 50;

    private const int Height = 50;

    private const int TileWidth = 18;

    private const int TileHeight = 18;

    private int[,] _grid = new int[50, 50];

    private SpriteBatch _spriteBatch;
    
    private Texture2D _texture;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = Width * TileWidth,
            PreferredBackBufferHeight = Height * TileHeight
        };

        Content.RootDirectory = "./09";

        IgnoreQueueLimit = true;
    }
    
    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            1 => new Part1(this),
            _ => throw new VisualisationParameterException()
        };
    }

    protected override void Initialize()
    {
        IsMouseVisible = true;
        
        _texture = new Texture2D(GraphicsDeviceManager.GraphicsDevice, Width * TileWidth, Height * TileHeight);

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        base.Initialize();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        base.Draw(gameTime);
    }
}