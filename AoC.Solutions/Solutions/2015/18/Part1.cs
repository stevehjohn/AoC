using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 100; i++)
        {
            RunStep();
        }

        return Lights.Count.ToString();
    }
}