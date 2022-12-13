using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._12;

public class Part1 : Base
{
    public Part1()
    {
    }

    public Part1(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }

    public override string GetAnswer()
    {
        ParseInput();

        var steps = FindShortestPath();

        return steps.ToString();
    }
}