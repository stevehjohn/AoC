using AoC.Solutions.Solutions._2025._12;
using AoC.Visualisations.Exceptions;
using AoC.Visualisations.Infrastructure;

namespace AoC.Visualisations.Visualisations._2025._12;

public class Visualisation : VisualisationBase<PuzzleState>
{
    public override void SetPart(int part)
    {
        Puzzle = part switch
        {
            1 => new Part1(this),
            _ => throw new VisualisationParameterException()
        };
    }
}