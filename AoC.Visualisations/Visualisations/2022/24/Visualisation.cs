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

    private (int X, int Y) _elfPosition;

    private (int X, int Y) _elfTarget;

    private Texture2D _tiles;
    
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

        _tiles = Content.Load<Texture2D>("tiles");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState)
        {
            var state = GetNextState();

            _map = state.Map;

            _moves = state.Moves;

            _width = (_map.GetUpperBound(0) + 1) * TileWidth;

            _height = (_map.GetUpperBound(1) + 1) * TileHeight;

            CreateBlizzards();
        }

        if (_moves == null)
        {
            return;
        }

        _elfPosition = (_elfPosition.X.Converge(_elfTarget.X), _elfPosition.Y.Converge(_elfTarget.Y));

        if (_frame % TileWidth == 0)
        {
            _move++;

            if (_move < _moves.Count)
            {
                var move = _moves[_move];
                
                if (_move == 0)
                {
                    _elfPosition = (move.X * TileWidth, move.Y * TileHeight);
                }

                _elfTarget = (move.X * TileWidth, move.Y * TileHeight);
            }
        }
        
        MoveBlizzards();
        
        _frame++;
        
        base.Update(gameTime);
    }

    private void MoveBlizzards()
    {
        _blizzards.ForAll((i, b) =>
        {
            var x = b.X + b.Dx;

            if (x < TileWidth)
            {
                x += _width - TileWidth * 2;
            }

            if (x >= _width - TileWidth)
            {
                x -= _width - TileWidth * 2;
            }

            var y = b.Y + b.Dy;

            if (y < TileHeight)
            {
                y += _height - TileHeight * 2;
            }

            if (y >= _height - TileHeight)
            {
                y -= _height - TileHeight * 2;
            }

            _blizzards[i] = (x, y, b.Dx, b.Dy);
        });
    }

    private void CreateBlizzards()
    {
        _map.ForAll((x, y, c) =>
        {
            if (c == '#' || c == '.')
            {
                return;
            }

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

        if (_map == null)
        {
            return;
        }

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
        
        DrawMap();

        DrawBlizzards();

        DrawElf();
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawMap()
    {
        _map.ForAll((x, y, c) =>
        {
            if (c == '#')
            {
                _spriteBatch.Draw(_tiles, 
                    new Vector2(x * TileWidth, y * TileWidth), 
                    new Rectangle(0, 0, TileWidth, TileHeight), 
                    Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5f);
            }
        });
    }

    private void DrawBlizzards()
    {
        _blizzards.ForAll((_, b) =>
        {
            _spriteBatch.Draw(_tiles, 
                new Vector2(b.X * TileWidth, b.Y * TileWidth), 
                new Rectangle(TileWidth, 0, TileWidth, TileHeight), 
                Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5f);
        });
    }

    private void DrawElf()
    {
        _spriteBatch.Draw(_tiles, 
            new Vector2(_elfPosition.X * TileWidth, _elfPosition.Y * TileWidth), 
            new Rectangle(TileWidth * 2, 0, TileWidth, TileHeight), 
            Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5f);
    }
}