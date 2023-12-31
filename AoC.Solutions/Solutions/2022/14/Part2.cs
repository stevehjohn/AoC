using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._14;

public class Part2 : Base
{
    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }

    public override string GetAnswer()
    {
        CreateCave();

        AddFloor();

        var result = SimulateSand();

        return result.ToString();
    }
}