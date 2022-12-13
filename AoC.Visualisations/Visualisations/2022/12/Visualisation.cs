using AoC.Solutions.Solutions._2022._12;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace AoC.Visualisations.Visualisations._2022._12;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private SpriteBatch _spriteBatch;

    private Texture2D _tile;

    private PuzzleState _state;

    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
                                {
                                    PreferredBackBufferWidth = Constants.ScreenWidth,
                                    PreferredBackBufferHeight = Constants.ScreenHeight
                                };

        // Something funky going on with having to add \bin\Windows - investigate.
        Content.RootDirectory = "_Content\\2022\\12\\bin\\Windows";
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

        _tile = Content.Load<Texture2D>("tile");

        base.LoadContent();
    }

    protected override void Draw(GameTime gameTime)
    {
        if (HasNextState)
        {
            _state = GetNextState();
        }

        _spriteBatch.Begin();

        DrawMap();

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawMap()
    {
        for (var y = 0; y < _state.Map.GetLength(1); y++)
        {
            var xOffset = 33;

            if (y % 2 == 0)
            {
                xOffset = 0;
            }

            for (var x = 0; x < _state.Map.GetLength(0); x++)
            {
                _spriteBatch.Draw(_tile, new Vector2(x * 49 + xOffset, y * 15 + x * 15), new Rectangle(0, 0, 47, 29), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
            }
        }
    }
}