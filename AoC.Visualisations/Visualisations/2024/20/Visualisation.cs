using AoC.Solutions.Common;
using AoC.Solutions.Solutions._2024._20;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = System.Drawing.Color;

namespace AoC.Visualisations.Visualisations._2024._20;

public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int TileWidth = 16;

    private const int TileHeight = 12;

    private const int FrameDelay = 4;

    private readonly Color[] _data = new Color[PuzzleState.Size * TileWidth * PuzzleState.Size * TileHeight];

    private readonly Queue<PuzzleState> _stateQueue = [];

    private PuzzleState _state;

    private Texture2D _texture;

    private SpriteBatch _spriteBatch;

    private char[,] _map;

    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            1 => new Part1(this),
            2 => new Part2(this),
            _ => throw new VisualisationParameterException()
        };
    }

    protected override void Initialize()
    {
        IsMouseVisible = true;

        _texture = new Texture2D(GraphicsDeviceManager.GraphicsDevice, PuzzleState.Size * TileWidth, PuzzleState.Size * TileHeight);

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            _stateQueue.Enqueue(GetNextState());

            if (_map == null)
            {
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Array.Fill(_data, Color.Black);
    }
}