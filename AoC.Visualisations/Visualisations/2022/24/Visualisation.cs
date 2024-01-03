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

    private readonly List<(int X, int Y)> _moves = [];

    private int _move = -1;

    private int _frame;

    private int _elfFrame;

    private SpriteBatch _spriteBatch;

    private readonly List<(int X, int Y, int Dx, int Dy)> _blizzards = [];

    private int _blizzardWidth;

    private int _blizzardHeight;

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

            case 2:
                Puzzle = new Part2(this);

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

            if (_map == null)
            {
                _map = state.Map;

                _blizzardWidth = (_map.GetUpperBound(0) - 1) * TileWidth;

                _blizzardHeight = (_map.GetUpperBound(1) - 1) * TileHeight;

                CreateBlizzards();
            }

            _moves.AddRange(state.Moves);
        }

        if (_moves.Count == 0)
        {
            return;
        }

        _elfPosition = (_elfPosition.X.Converge(_elfTarget.X), _elfPosition.Y.Converge(_elfTarget.Y).Converge(_elfTarget.Y));

        if (_elfFrame == 0)
        {
            _move++;

            if (_move > - 1 &&_move < _moves.Count)
            {
                var move = _moves[_move];
                
                if (_move == 0)
                {
                    _elfPosition = (move.X * TileWidth, move.Y * TileHeight);
                }

                _elfTarget = (move.X * TileWidth, move.Y * TileHeight);
            }
        }

        _elfFrame++;

        if (_elfFrame >= TileWidth)
        {
            _elfFrame = 0;
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

            if (x < 0)
            {
                x += _blizzardWidth;
            }

            if (x >= _blizzardWidth)
            {
                x -= _blizzardWidth;
            }

            var y = b.Y + b.Dy;

            if (y < 0)
            {
                y += _blizzardHeight;
            }

            if (y >= _blizzardHeight)
            {
                y -= _blizzardHeight;
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
            
            _blizzards.Add(((x - 1) * TileWidth, (y - 1) * TileHeight, dX, dY * 2));
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
                    new Vector2(x * TileWidth, y * TileHeight), 
                    new Rectangle(0, 0, TileWidth, TileHeight), 
                    Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.9f);
            }
        });
    }

    private void DrawBlizzards()
    {
        var z = 0.0001f;
        
        _blizzards.ForAll((_, b) =>
        {
            // TODO: Draw the wrap around.
            
            _spriteBatch.Draw(_tiles, 
                new Vector2(b.X + TileWidth, b.Y + TileHeight), 
                new Rectangle(TileWidth, 0, TileWidth, TileHeight), 
                Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, z);
            
            z += 0.0001f;
        });
    }

    private void DrawElf()
    {
        _spriteBatch.Draw(_tiles, 
            new Vector2(_elfPosition.X, _elfPosition.Y), 
            new Rectangle(TileWidth * 2, 0, TileWidth, TileHeight), 
            Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
    }
}