using AoC.Solutions.Solutions._2024._09;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace AoC.Visualisations.Visualisations._2024._09;

[UsedImplicitly]
public class Visualisation : VisualisationBase<PuzzleState>
{
    private const int GridSize = 308;
    
    public Visualisation()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = GridSize * 3,
            PreferredBackBufferHeight = GridSize * 3
        };

        Content.RootDirectory = "./09";

        IgnoreQueueLimit = true;
    }
    
    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            2 => new Part2(this),
            _ => throw new VisualisationParameterException()
        };
    }
}