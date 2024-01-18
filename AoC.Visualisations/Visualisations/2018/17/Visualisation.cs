using AoC.Solutions.Solutions._2018._17;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2018._17;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int ScreenWidth = 1536;

    private const int ScreenHeight = 1152;

    private const int TileSize = 8;

    private const int TileMapWidth = 11;

    private SpriteBatch _spriteBatch;

    private Texture2D _tiles;

    private PuzzleState _state;

    private char[,] _previousPuzzleState;

    private Map _map;

    private float _y;

    private int _frame = 4;

    private int _waterFrame;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = ScreenWidth,
                                     PreferredBackBufferHeight = ScreenHeight
                                 };

        Content.RootDirectory = "./17";
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

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tiles = Content.Load<Texture2D>("tiles");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (HasNextState && _frame == 4)
        {
            if (_state != null)
            {
                _previousPuzzleState = new char[_map.Width, _map.Height];

                Buffer.BlockCopy(_state.Map, 0, _previousPuzzleState, 0, sizeof(char) * _map.Width *_map.Height);
            }

            _state = GetNextState();

            if (_map == null)
            {
                _map = new Map();

                _map.CreateMap(_state.Map);
            }

            _frame = 0;
        }

        _frame++;

        if (_y < 0)
        {
            _y = 0;
        }

        // ReSharper disable once PossibleLossOfFraction
        if (_y >= _map.Height - ScreenHeight / TileSize)
        {
            // ReSharper disable once PossibleLossOfFraction
            _y = _map.Height - ScreenHeight / TileSize;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(9, 10, 17));

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        DrawMap();

        DrawWater();

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawMap()
    {
        for (var y = 0; y < ScreenHeight / TileSize; y++)
        {
            for (var x = 0; x < _map.Width; x++)
            {
                var tile = _map[x, y + (int) _y];

                if (tile > 0)
                {
                    var row = (tile - 1) / TileMapWidth;

                    var column = (tile - 1) % TileMapWidth;

                    _spriteBatch.Draw(_tiles, new Vector2(x * TileSize, y * TileSize), new Rectangle(column * TileSize, row * TileSize, TileSize, TileSize), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                }
            }
        }
    }

    private void DrawWater()
    {
        var lastY = 0;

        for (var y = 0; y < ScreenHeight / TileSize; y++)
        {
            for (var x = 0; x < _map.Width; x++)
            {
                var tile = _state.Map[x, y + (int) _y];

                switch (tile)
                {
                    case '|':
                    {
                        _spriteBatch.Draw(_tiles, new Vector2(x * TileSize, y * TileSize),
                            _state.Map[x, y + (int) _y - 1] == '|'
                                ? new Rectangle(_waterFrame / 5 * TileSize, 5 * TileSize, TileSize, TileSize)
                                : new Rectangle((5 + _waterFrame / 10) * TileSize, 5 * TileSize, TileSize, TileSize), Color.White * 0.6f, 0, Vector2.Zero,
                            Vector2.One, SpriteEffects.None, 0.5f);

                        if (y > lastY)
                        {
                            lastY = y;
                        }

                        break;
                    }
                    case '~':
                    {
                        if (_previousPuzzleState != null && _previousPuzzleState[x, y + (int) _y] == '\0')
                        {
                            _spriteBatch.Draw(_tiles, new Vector2(x * TileSize, y * TileSize), new Rectangle((6 + _frame) * TileSize, 5 * TileSize, TileSize, TileSize), Color.White * 0.6f, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5f);
                        }
                        else
                        {
                            if (_state.Map[x, y + (int)_y - 1] == '\0')
                            {
                                _spriteBatch.Draw(_tiles, new Vector2(x * TileSize, y * TileSize), new Rectangle(4 * TileSize, 5 * TileSize, TileSize, TileSize), Color.White * 0.6f, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5f);

                                _spriteBatch.Draw(_tiles, new Vector2(x * TileSize, (y - 1) * TileSize), new Rectangle((5 + _waterFrame / 10) * TileSize, 5 * TileSize, TileSize, TileSize), Color.White * 0.6f, 0, Vector2.Zero, Vector2.One,
                                    SpriteEffects.None, .5f);
                            }
                            else
                            {
                                _spriteBatch.Draw(_tiles, new Vector2(x * TileSize, y * TileSize), new Rectangle(4 * TileSize, 5 * TileSize, TileSize, TileSize), Color.White * 0.6f, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5f);
                            }
                        }

                        if (y > lastY)
                        {
                            lastY = y;
                        }

                        break;
                    }
                }
            }
        }

        if (lastY > ScreenHeight / TileSize / 4 * 3)
        {
            _y++;
        }

        _waterFrame++;

        if (_waterFrame > 19)
        {
            _waterFrame = 0;
        }
    }
}