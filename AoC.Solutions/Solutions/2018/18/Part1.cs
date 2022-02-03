using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 10; i++)
        {
            RunCycle();
        }

        return GetResourceValue().ToString();
    }
}