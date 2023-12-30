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