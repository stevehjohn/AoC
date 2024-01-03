using AoC.Solutions.Solutions._2022._24;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace AoC.Visualisations.Visualisations._2022._24;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 980,
            PreferredBackBufferHeight = 980
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
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}