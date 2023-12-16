using AoC.Solutions.Solutions._2019._18;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2019._18;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private SpriteBatch _spriteBatch;

    private Texture2D _tiles;

    private PuzzleState _state;

    private readonly Color[] _colors = 
    {
        Color.Blue,
        Color.Red,
        Color.Magenta,
        Color.Green,
        Color.Cyan,
        Color.Yellow,
        Color.White
    };

    private int _frame;

    private int _color;

    private readonly Willy[] _willys = new Willy[4];
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                 {
                                     PreferredBackBufferWidth = 648,
                                     PreferredBackBufferHeight = 656
                                 };

        Content.RootDirectory = "./18";
    }

    public override void SetPart(int part)
    {
        switch (part)
        {
            case 2:
                Puzzle = new Part2(this);

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
        if (HasNextState)
        {
            _state = GetNextState();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _frame++;
        
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        DrawMap();

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawMap()
    {
        if (_frame % 5 == 0)
        {
            _color++;

            if (_color == _colors.Length)
            {
                _color = 0;
            }
        }

        var keyColor = _colors[_color];

        for (var y = 0; y < _state.Map.GetLength(1); y++)
        {
            for (var x = 0; x < _state.Map.GetLength(0); x++)
            {
                var tile = _state.Map[x, y];

                if (tile == '#')
                {
                    _spriteBatch.Draw(_tiles, new Vector2(x * 8, y * 8), new Rectangle(0, 0, 8, 8), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    
                    continue;
                }

                if (char.IsLetter(tile))
                {
                    if (tile == char.ToLower(tile))
                    {
                        _spriteBatch.Draw(_tiles, new Vector2(x * 8, y * 8), new Rectangle(16, 0, 8, 8), keyColor, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                        
                        continue;
                    }

                    _spriteBatch.Draw(_tiles, new Vector2(x * 8, y * 8), new Rectangle(8, 0, 8, 8), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                }
            }
        }
    }
}