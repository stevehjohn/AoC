using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._24;

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

        var answer = RunSimulation(3);

        return answer.ToString();
    }
}