using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._17;

[UsedImplicitly]
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
        ParseInput();
        
        return Solve(4, 10).ToString();
    }
}