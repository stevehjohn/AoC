using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._17;

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
        return GetAnswer(false).ToString();
    }
}