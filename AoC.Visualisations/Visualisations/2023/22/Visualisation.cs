using AoC.Solutions.Solutions._2023._22;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AoC.Visualisations.Visualisations._2023._22;

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
            PreferredBackBufferWidth = 100,
            PreferredBackBufferHeight = 980
        };

        Content.RootDirectory = "./22";
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

        _tile = Content.Load<Texture2D>("tile");
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
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
        
        DrawBricks();
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawBricks()
    {
        for (var z = 1; z < _state.Height; z++)
        {
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    var id = _state.Map[z, x, y];

                    if (id > 0)
                    {
                        _spriteBatch.Draw(_tile, new Vector2(20 + x * 3 + y * 3, 960 - 7 * z + y * 3), new Rectangle(0, 0, 7, 9),
                            GetBrickColor(id), 0, Vector2.Zero, Vector2.One, SpriteEffects.None, .9f);
                    }
                }
            }
        }
    }

    private static Color GetBrickColor(int id)
    {
        return (id % 8) switch
        {
            1 => Color.FromNonPremultiplied(0, 0, 255, 255),
            2 => Color.FromNonPremultiplied(255, 0, 0, 255),
            3 => Color.FromNonPremultiplied(255, 0, 255, 255),
            4 => Color.FromNonPremultiplied(0, 255, 0, 255),
            5 => Color.FromNonPremultiplied(0, 255, 255, 255),
            6 => Color.FromNonPremultiplied(255, 255, 0, 255),
            7 => Color.FromNonPremultiplied(255, 255, 255, 255),
            _ => Color.FromNonPremultiplied(0, 0, 0, 255)
        };
    }
}