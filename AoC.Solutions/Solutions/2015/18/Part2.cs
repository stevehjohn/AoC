using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        Lights.Add(new Point(0, 0));

        Lights.Add(new Point(0, 99));

        Lights.Add(new Point(99, 0));

        Lights.Add(new Point(99, 99));

        for (var i = 0; i < 100; i++)
        {
            RunStep(true);
        }

        return Lights.Count.ToString();
    }
}