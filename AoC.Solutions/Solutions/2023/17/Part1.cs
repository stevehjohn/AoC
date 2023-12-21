using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._17;

[UsedImplicitly]
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
        
        return Solve(0, 3).ToString();
    }
}