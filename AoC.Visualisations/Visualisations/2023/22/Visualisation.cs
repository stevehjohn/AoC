using AoC.Solutions.Solutions._2023._22;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace AoC.Visualisations.Visualisations._2023._22;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
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
}