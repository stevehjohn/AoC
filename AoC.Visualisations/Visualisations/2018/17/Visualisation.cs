using AoC.Solutions.Solutions._2018._17;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2018._17;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int ScreenWidth = 1536;

    private const int ScreenHeight = 1152;

    private const int TileSize = 8;

    private const int TileMapWidth = 11;

    // ReSharper disable once NotAccessedField.Local
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private SpriteBatch _spriteBatch;

    private Texture2D _tiles;

    private PuzzleState _state;

    private Map _map;

    private int _y;

    private MouseState? _previousMouseState;

    private int _frame;

    private int _waterFrame;

    public Visualisation()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = ScreenWidth,
                                     PreferredBackBufferHeight = ScreenHeight
                                 };

        // Something funky going on with having to add \bin\Windows - investigate.
        Content.RootDirectory = "_Content\\2018\\17\\bin\\Windows";
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
        if (HasNextState && _frame == 0)
        {
            _state = GetNextState();

            if (_map == null)
            {
                _map = new Map();

                _map.CreateMap(_state.Map);
            }

            _frame = 1;
        }

        _frame--;

        var mouseState = Mouse.GetState();

        if (_previousMouseState != null)
        {
            _y += (_previousMouseState.Value.ScrollWheelValue - mouseState.ScrollWheelValue) / 120;

            if (_y < 0)
            {
                _y = 0;
            }

            if (_y >= _map.Height - ScreenHeight / TileSize)
            {
                _y = _map.Height - ScreenHeight / TileSize;
            }
        }

        _previousMouseState = mouseState;

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
                var tile = _map[x, y + _y];

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
                var tile = _state.Map[x, y + _y];

                if (tile == '|')
                {
                    if (_state.Map[x, y + _y - 1] == '|')
                    {
                        _spriteBatch.Draw(_tiles, new Vector2(x * TileSize, y * TileSize), new Rectangle(_waterFrame / 10 * TileSize, 5 * TileSize, TileSize, TileSize), Color.White * 0.75f, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
                    }
                    else
                    {
                        _spriteBatch.Draw(_tiles, new Vector2(x * TileSize, y * TileSize), new Rectangle(5 * TileSize, 5 * TileSize, TileSize, TileSize), Color.White * 0.75f, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
                    }

                    if (y > lastY)
                    {
                        lastY = y;
                    }
                }

                if (tile == '~')
                {
                    _spriteBatch.Draw(_tiles, new Vector2(x * TileSize, y * TileSize), new Rectangle(4 * TileSize, 5 * TileSize, TileSize, TileSize), Color.White * 0.75f, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);

                    if (y > lastY)
                    {
                        lastY = y;
                    }
                }
            }
        }

        //if (_y + lastY > ScreenHeight / 3 / TileSize)
        //{
        //    _y = lastY + ScreenHeight / 3 / TileSize;
        //}

        _waterFrame++;

        if (_waterFrame > 39)
        {
            _waterFrame = 0;
        }
    }
}