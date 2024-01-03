using AoC.Solutions.Solutions._2022._24;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2022._24;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private char[,] _map;

    private List<(int X, int Y)> _moves;

    private int _move = -1;

    private int _frame;

    private SpriteBatch _spriteBatch;
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1464,
            PreferredBackBufferHeight = 648
        };

        Content.RootDirectory = "./24";
    }
    
    public override void SetPart(int part)
    {
        switch (part)
        {
            case 1:
                Puzzle = new Part1(this);

                break;

            default:
                throw new VisualisationParameterException();
        }
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // _tiles = Content.Load<Texture2D>("tiles");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            var state = GetNextState();

            _map = state.Map;

            _moves = state.Moves;
        }

        if (_moves == null)
        {
            return;
        }

        if (_frame % 12 == 0)
        {
            _move++;

            if (_move < _moves.Count)
            {
                // Move elf
            }
        }
        
        // Move blizzards

        _frame++;
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        DrawMap();

        DrawBlizzards();

        DrawElf();
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawMap()
    {
    }

    private void DrawBlizzards()
    {
    }

    private void DrawElf()
    {
    }
}