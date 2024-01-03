using AoC.Solutions.Extensions;
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
    private const int TileWidth = 12;

    private const int TileHeight = 24;
    
    private char[,] _map;

    private List<(int X, int Y)> _moves;

    private int _move = -1;

    private int _frame;

    private SpriteBatch _spriteBatch;

    private readonly List<(int X, int Y, int Dx, int Dy)> _blizzards = [];

    private int _width;

    private int _height;
    
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

            CreateBlizzards();
        }

        if (_moves == null)
        {
            return;
        }

        if (_frame % TileWidth == 0)
        {
            _move++;

            if (_move < _moves.Count)
            {
                // Move elf
            }
        }
        
        _blizzards.ForAll((i, b) =>
        {
            var x = b.X + b.Dx;
            var y = b.Y + b.Dy;

            _blizzards[i] = (x, y, b.Dx, b.Dy);
        });

        _frame++;
        
        base.Update(gameTime);
    }

    private void CreateBlizzards()
    {
        _map.ForAll((x, y, c) =>
        {
            var dX = c switch
            {
                '<' => -1,
                '>' => 1,
                _ => 0
            };
            
            var dY = c switch
            {
                '^' => -1,
                'v' => 1,
                _ => 0
            };
            
            _blizzards.Add((x * TileWidth, y * TileHeight, dX, dY * 2));
        });
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